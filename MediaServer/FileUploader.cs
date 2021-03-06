﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
namespace MediaServer
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUploader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileBase64">文件的base64编码</param>
        /// <param name="originalName">文件原名</param>
        /// <param name="localSavePathRoot">本地存储地址</param>
        /// <param name="domainType">文件的域类型</param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string Upload(string fileBase64, string originalName, string localSavePathRoot, string domainType, FileType fileType)
        {
            originalName = Path.GetFileName(originalName);
            fileBase64 = fileBase64.Replace(" ", "+");
            byte[] fileData = Convert.FromBase64String(fileBase64);
            string savedPath = string.Empty;
            string relativePath = ServerSettings.DomainPath[domainType];
            string fileName = ServerSettings.FileNameBuilder(originalName, domainType, fileType);
            string fullLocalPath = localSavePathRoot + relativePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();
            return  fileName;
        }

        public static string UploadFromUrl(string url, string originalName, string localSavePathRoot, string domainType, FileType fileType)
        {
            originalName = Path.GetFileName(originalName);
            var client = new WebClient();
            byte[] fileData = client.DownloadData(url); ; 
            string savedPath = string.Empty;
            string relativePath = ServerSettings.DomainPath[domainType];
            string fileName = ServerSettings.FileNameBuilder(originalName, domainType, fileType);
            string fullLocalPath = localSavePathRoot + relativePath + fileName;
            PHSuit.IOHelper.EnsureFileDirectory(fullLocalPath);
            FileStream fs = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Flush();
            fs.Close();
            return fileName;
        }
    }
}



 
