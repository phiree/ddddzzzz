﻿using System;
using System.Collections.Generic;
 
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM002002 : BaseResponse
{
    public ResponseORM002002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM002002 requestData = this.request.ReqData.ToObject<ReqDataORM002002>();

 
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllOrder = new BLLServiceOrder();
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

                RespDataORM002002 respData = new RespDataORM002002();
                string reqOrderId = requestData.orderID;
                Guid order_ID = new Guid(requestData.orderID);
                ServiceOrder orderToReturn = bllOrder.GetOne(order_ID);
                if (orderToReturn == null)
                {
                    this.state_CODE = Dicts.StateCode[2];
                    this.err_Msg = "没有该订单";
                    return;
                }
                if (orderToReturn.Customer != member)
                {
                    this.state_CODE = Dicts.StateCode[2];
                    this.err_Msg = "该订单不是您创建的";
                    return;
                }
                respData.orderID = orderToReturn.Id.ToString();
                RespDataORM002002_payObj payObj = new RespDataORM002002_payObj().Adap(orderToReturn);
                respData.payObj = payObj;
                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }

}

public class ReqDataORM002002
{
    
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    

}
public class RespDataORM002002
{
    public RespDataORM002002()
    {
        orderID = string.Empty;
    }
    public string orderID { get; set; }
    public RespDataORM002002_payObj payObj { get; set; }
 
}
public class RespDataORM002002_payObj
{
    public string type { get; set; }
 
    public string url { get; set; }
    public RespDataORM002002_payObj Adap(ServiceOrder order)
    {
        this.type = "alipay";
        this.url = order.BuildPayLink(
            Dianzhu.Config.Config.GetAppSetting("PayServer")
            );
         
        return this;
    }
}

 


