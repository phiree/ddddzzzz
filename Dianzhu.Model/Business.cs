﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace Dianzhu.Model
{
    /// <summary>
    /// 商户基类.泛指可以提供服务的单位,可以是公司 也可以是个人
    /// </summary>
    public class Business_Abs
    {
        public Business_Abs()
        {
            Enabled = true;
        }
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public virtual string Contact { get; set; }
        /// <summary>
        /// 公司电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 公司邮箱
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 公司网址
        /// </summary>
        public virtual string WebSite { get; set; }

        /// <summary>
        /// 商户所有者.
        /// </summary>
        public virtual DZMembership Owner { get; set; }
        public virtual bool Enabled { get; set; }

    }

    /// <summary>
    /// 商家
    /// </summary>
    public class Business : Business_Abs
    {
        public Business()
        {
            AreaServiceTo = new List<Area>();
            BusinessImages = new List<BusinessImage>();
            ServiceType = new List<ServiceType>();
            CashTicketTemplates = new List<CashTicketTemplate>();
            
        }
        /// <summary>
        ///  所在辖区
        /// </summary>
        public virtual Area AreaBelongTo { get; set; }
        /// <summary>
        ///  服务范围
        /// </summary>
        public virtual IList<Area> AreaServiceTo { get; set; }
        /// <summary>
        ///  地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 员工总人数
        /// </summary>
        public virtual int StaffAmount { get; set; }
        /// <summary>
        /// 从业时长, 比如 1915年进入该行业的, 则是 百  --年老 --店,该值为100.
        /// </summary>
        public virtual int WorkingYears { get; set; }
        /// <summary>
        /// 商家相关的图片
        /// </summary>
        public virtual IList<BusinessImage> BusinessImages { get; set; }
        /// <summary>
        /// 服务类型.
        /// </summary>
        public virtual IList<ServiceType> ServiceType { get; set; }

       
        /// <summary>
        /// 店铺地理坐标,精度，维度。
        /// </summary>
        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }

        public virtual string RawAddressFromMapAPI { get; set; }
        /// <summary>
        /// 优惠推广的范围.所有的优惠券都是一样的.
        /// </summary>
        public virtual double PromoteScope { get; set; }
        /// <summary>
        /// 是否通过了审核.
        /// </summary>
        public virtual bool IsApplyApproved { get; set; }
        /// <summary>
        /// 审核拒绝信息.
        /// </summary>
        public virtual string ApplyRejectMessage { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public virtual DateTime DateApply { get; set; }
       
        /// <summary>
        /// 审核通过日期
        /// </summary>
        public virtual DateTime DateApproved { get; set; }
        public virtual BusinessImage BusinessLicence
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                BusinessImages.Add(value);
            }
        }
        public virtual BusinessImage BusinessAvatar
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Avatar).OrderByDescending(x=>x.UploadTime).ToArray();
                if (bi.Count() >= 1)
                    return bi[0];
                return new BusinessImage();
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Avatar).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                BusinessImages.Add(value);
            }
        }
        public virtual Enums.enum_IDCardType ChargePersonIdCardType
        {
            get;
            set;
        }
        public virtual string ChargePersonIdCardNo
        {
            get;
            set;
        }
        public virtual IList<BusinessImage> ChargePersonIdCards
        {
            get
            {
                BusinessImage[] bi = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).ToArray();
                return bi;
            }
            set
            {

                IList<BusinessImage> images = BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).ToList();
                foreach (BusinessImage i in images)
                {
                    BusinessImages.Remove(i);
                }
                foreach(BusinessImage i in value)
                {
                BusinessImages.Add(i);
                }
            }

        }

        /// <summary>
        /// 负责人照片
        /// </summary>
        public virtual IList<BusinessImage> BusinessChargePersonIdCards
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_ChargePersonIdCard).OrderBy(x => x.UploadTime).ToList();
            }
        }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual IList<BusinessImage> BusinessLicenses
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_License).OrderBy(x => x.UploadTime).ToList();
            }
        }

        /// <summary>
        /// 店铺图片展示
        /// </summary>
        public virtual IList<BusinessImage> BusinessShows
        {
            get
            {
                return BusinessImages.Where(x => x.ImageType == Enums.enum_ImageType.Business_Show).OrderBy(x => x.UploadTime).ToList();
            }

        }
        /// <summary>
        /// 资料完成度
        /// </summary>
        public virtual int CompetePercent
        {
            get {
                int percent = 0;
                if (!string.IsNullOrEmpty(this.Address))
                {
                    percent += 7;
                }
                if ( this.AreaBelongTo!=null)
                {
                    percent += 14;
                }
                
                if (this.BusinessImages.Count>0)
                {
                    percent += 7;
                }
                if (this.BusinessLicence!=null)//5
                {
                    percent +=7;
                }
                if (this.BusinessShows.Count>0)
                {
                    percent += 7;
                }
                
                if (this.ChargePersonIdCards.Count>0)
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.ChargePersonIdCardNo))
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.Contact))//10
                {
                    percent += 7;
                }
                if (!string.IsNullOrEmpty(this.Description))
                {
                    percent += 7;
                }

                if (!string.IsNullOrEmpty(this.Email))
                {
                    percent += 7;
                }

                if (!string.IsNullOrEmpty(this.Name))
                {
                    percent += 10;
                }
                if (!string.IsNullOrEmpty(this.Phone))
                {
                    percent += 10;
                }
                 
                if (this.WorkingYears>0)
                {
                    percent += 10;
                }


                return percent;
            }
        }
        public virtual IList<CashTicketTemplate> CashTicketTemplates
        {
            get;
            set;
        }

        public virtual IList<CashTicket> CashTickets
        {
            get {
                IList<CashTicket> cashtickets = new List<CashTicket>();
                foreach (CashTicketTemplate temp in CashTicketTemplates)
                {
                    foreach (CashTicket cashticket in temp.CashTickets)
                    {
                        cashtickets.Add(cashticket);
                    }
                }
                return cashtickets;
            }
        }
        /// <summary>
        /// 可供分配的现金券
        /// </summary>
        public virtual IList<CashTicket> CashTicketsAvaiableToAssign
        {
            get
            {
                IList<CashTicket> cashtickets = new List<CashTicket>();
                foreach (CashTicketTemplate temp in CashTicketTemplates)
                {
                    if (!temp.Enabled) continue;
                    foreach (CashTicket cashticket in temp.CashTickets)
                    {
                        if (cashticket.UserAssigned != null) continue;
                        cashtickets.Add(cashticket);
                    }
                }
                return cashtickets;
            }
        }
        /// <summary>
        /// 可供分配的现金券的总金额
        /// </summary>
        public virtual int CashTicketsAvaiableToAssignAmount
        {
            get
            {
                int amount = 0;
                IList<CashTicket> cashtickets = new List<CashTicket>();
                foreach (CashTicketTemplate temp in CashTicketTemplates)
                {
                    if (!temp.Enabled) continue;
                    foreach (CashTicket cashticket in temp.CashTickets)
                    {
                        if (cashticket.UserAssigned != null) continue;
                        amount += temp.Amount;
                         
                    }
                }
                return amount;
            }
        }

       

    }

    /// <summary>
    /// 商家的一些图片
    /// </summary>
    public class BusinessImage
    {
        public BusinessImage()
        {
            IsCurrent = false;
        }
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 图片名称,图片存储路径由配置确定.
        /// </summary>
        public virtual string ImageName { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public virtual DateTime UploadTime { get; set; }
        /// <summary>
        /// 序号,用于手动排序
        /// </summary>
        public virtual int OrderNumber { get; set; }
        /// <summary>
        /// 图片大小, 单位 KB
        /// </summary>
        public virtual int Size { get; set; }
        /// <summary>
        /// 图片的类型,
        /// </summary>
        public virtual Enums.enum_ImageType ImageType { get; set; }
        /// <summary>
        /// 是否是当前使用的图片
        /// </summary>
        public virtual bool IsCurrent { get; set; }
    }

}
