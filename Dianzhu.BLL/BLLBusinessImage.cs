﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web;
using System.IO;

namespace Dianzhu.BLL
{
   public class BLLBusinessImage
    {
       
       public DALBusinessImage DALBusinessImage=DALFactory.DALBusinessImage;
       public BLLBusiness BLLBusiness = new BLLBusiness();

       
       public void Delete(Guid biId)
       {
           BusinessImage bi = DALBusinessImage.GetOne(biId);
           string filePath = HttpContext.Current.Server.MapPath(SiteConfig.BusinessImagePath+"/original/")+bi.ImageName;
           if (File.Exists(filePath))
           {
               File.Delete(filePath);
           }
           DALBusinessImage.Delete(bi);
       }
       public string  Save(Guid businessId,System.Web.HttpPostedFileBase imageFile, Dianzhu.Model.Enums.enum_ImageType imageType)
       {
           Business b = BLLBusiness.GetOne(businessId);
           string savedPath = string.Empty;
           string imageName=string.Empty;
           if (imageFile != null && imageFile.ContentLength != 0)
           {
               imageName = businessId + imageType.ToString() + Guid.NewGuid().GetHashCode() + Path.GetExtension(imageFile.FileName);
               savedPath = HttpContext.Current.Server.MapPath(SiteConfig.BusinessImagePath + "/original/") + imageName;
               imageFile.SaveAs(savedPath);
               BusinessImage biImage = new BusinessImage
               {
                   ImageType= imageType,
                   UploadTime = DateTime.Now,
                   ImageName = imageName,
                   Size = imageFile.ContentLength
               };
               b.BusinessImages.Add(biImage);
           }
           BLLBusiness.Updte(b);
           return "/media/business/original/" + imageName;
       }

        public BusinessImage FindBusImageByName(string imgName)
        {
            return DALBusinessImage.FindBusImageByName(imgName);
        }

        public bool DeleteBusImageByName(string imgName)
        {
            BusinessImage bi = DALBusinessImage.FindBusImageByName(imgName);
            if (bi != null)
            {
                string filePath = HttpContext.Current.Server.MapPath(bi.GetRelativePathByType()) + bi.ImageName;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                DALBusinessImage.Delete(bi);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
