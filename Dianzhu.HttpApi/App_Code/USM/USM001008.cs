﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
/// <summary>
/// 用户设备认证
/// </summary>
public class ResponseUSM001008 : BaseResponse
{
    public ResponseUSM001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataUSM001008 requestData = this.request.ReqData.ToObject<ReqDataUSM001008>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.userID;

        try
        {
           
            
             DZMembership member;
             bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {
                //上传图片.
                //bllDeviceBind.UpdateDeviceBindStatus(member, requestData.appToken, requestData.appName);
                string ext = string.Empty;
                switch (requestData.FileType) {
                    case USM001008UploadedFileType.image:ext = ".png"; break;
                        
                    case USM001008UploadedFileType.video: ext = ".mp4"; break;
                    case USM001008UploadedFileType.voice: ext = ".mp3"; break;
                }
                string fileName = Guid.NewGuid() + ext;
                string relativePath = System.Configuration.ConfigurationManager.AppSettings["business_image_root"];
                string filePath = HttpContext.Current.Server.MapPath(relativePath);
                PHSuit.IOHelper.SaveFileFromBase64(requestData.Resource, filePath+fileName);
                this.state_CODE = Dicts.StateCode[0];
                RespDataUSM001008 respData = new RespDataUSM001008();
                respData.userID = requestData.userID;
                if(requestData.FileType== USM001008UploadedFileType.image)
                { 
                respData.ResourceUrl = ConfigurationManager.AppSettings["media_server"]+"imagehandler.ashx?imagename="+fileName;
                }
                else
                {
                    respData.ResourceUrl = ConfigurationManager.AppSettings["media_server"] + ConfigurationManager.AppSettings["business_image_root"] + fileName;
                }
                member.AvatarUrl = fileName;
                p.UpdateDZMembership(member);
                this.RespData = respData;
                
            }
            catch (Exception ex)
            { 
                this.state_CODE=ex.Message;   
            }
           
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;

        }

    }
    public override string BuildJsonResponse()
    {

        return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}

public class ReqDataUSM001008
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    public string Resource { get; set; }
    public string type { get; set; }
    public USM001008UploadedFileType FileType {
        get { 
                USM001008UploadedFileType fileType;
          bool isType=  Enum.TryParse<USM001008UploadedFileType>(type, out fileType);
            if (!isType) { throw new Exception("不可识别的文件类型"); }
            return fileType;
        }
    }


}
public enum USM001008UploadedFileType {
    voice,
    image,
    video
}
public class RespDataUSM001008
{
    public string userID { get; set; }
    public string ResourceUrl { get; set; }
}

 