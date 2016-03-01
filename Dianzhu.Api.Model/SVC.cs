﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
using Dianzhu.Model.Enums;

namespace Dianzhu.Api.Model
{
    public class RespDataSVC_svcObj
    {
        public string storeID { get; set; }
        public string svcID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string introduce { get; set; }
        public string area { get; set; }
        public string startAt { get; set; }
        public string unitPrice { get; set; }
        public string appointmentTime { get; set; }
        public string serviceTimes { get; set; }
        public string maxOrder { get; set; }
        public string doorService { get; set; }
        public string serviceObject { get; set; }
        public string payWay { get; set; }
        public string tag { get; set; }
        public string open { get; set; }
        public RespDataSVC_svcObj Adapt(DZService service, IList<DZTag> tags)
        {
            this.storeID = service.Business.Id.ToString();
            this.svcID = service.Id.ToString();
            this.name = service.Name;
            this.type = service.ServiceType.Name;
            this.introduce = service.Description;
            this.area = service.Business.AreaBelongTo.Name;
            this.startAt = service.MinPrice.ToString("0.0");
            this.unitPrice = service.UnitPrice.ToString("0.0");
            this.appointmentTime = service.OrderDelay.ToString();
            service.OpenTimes.ToList().ForEach(x => this.serviceTimes += x.Id + ",");
            this.serviceTimes = this.serviceTimes.TrimEnd(',');
            this.maxOrder = service.MaxOrdersPerDay.ToString();
            this.doorService = service.ServiceMode == enum_ServiceMode.ToHouse ? "Y" : "N";
            this.serviceObject = service.IsForBusiness ? "company" : "all";
            this.payWay = service.PayType == enum_PayType.Online ? "alipay" : "none";
            tags.ToList().ForEach(x => this.tag += x.Text);
            this.tag = this.tag.TrimEnd(',');
            this.open = service.Enabled ? "Y" : "N";
            return this;

        }
        public DZService Adap(RespDataSVC_svcObj svcObj)
        {
            throw new NotImplementedException();
        }
    }

    public class ReqDataSVC001001
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public RespDataSVC_svcObj svcObj { get; set; }
    }
}