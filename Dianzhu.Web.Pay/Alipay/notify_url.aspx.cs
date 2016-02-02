﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using Com.Alipay;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Pay;

public partial class notify_url : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    protected void Page_Load(object sender, EventArgs e)
    {
        log.Debug("支付完成，调用notifyurl");
        //保存支付接口返回的原始数据
        BLLPaymentLog bllPaymentLog = new BLLPaymentLog();
        BLLServiceOrder bllOrder = new BLLServiceOrder();
        PaymentLog paymentLog = new PaymentLog();
        paymentLog.ApiString = Request.Url + "|" + Request.QueryString.ToString() + "|" + Request.Form.ToString();
        paymentLog.PaylogType = Dianzhu.Model.Enums.enum_PaylogType.ResultNotifyFromAli;
        paymentLog.LogTime = DateTime.Now;
        log.Debug("保存支付记录");
        bllPaymentLog.SaveOrUpdate(paymentLog);

        BLLPayment bllPayment = new BLLPayment();
        Payment payment = null;
        ServiceOrder order = null;
        Guid paymentId;
        bool isPaymentGuid = Guid.TryParse(Request["out_trade_no"], out paymentId);
        string tradeNo = Request["trade_no"];
        log.Debug("2");
        if (isPaymentGuid)
        {

            payment = bllPayment.GetOne(paymentId);

            //更新order
            paymentLog.PaymentId = paymentId;
            bllPaymentLog.SaveOrUpdate(paymentLog);
            log.Debug("3");
            if (payment == null)
            {
                Response.Write("fail");
            }
            payment.TradeNo = tradeNo;
            log.Debug("4");
            bllPayment.Update(payment);

        }
        else
        {
            log.Error("支付项Id不是guid格式");
            Response.Write("fail");
        }
        SortedDictionary<string, string> sPara = GetRequestGet();
        if (sPara.Count > 0)//判断是否有带返回参数
        {
            Notify aliNotify = new Notify();
            bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);
            log.Debug("5");
            if (verifyResult)//验证成功
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //请在这里加上商户的业务逻辑程序代码

                //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                string total_fee = Request.QueryString["total_fee"];            //获取总金额
                string subject = Request.QueryString["subject"];                //商品名称、订单名称
                string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                string trade_status = Request.QueryString["trade_status"];      //交易状态             

                if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                {
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //如果有做过处理，不执行商户的业务程序

                    order = payment.Order;

                    // bllOrder.ChangeStatus(order, Dianzhu.Model.Enums.enum_OrderStatus.Payed);
                    switch (order.OrderStatus)
                    {
                        case Dianzhu.Model.Enums.enum_OrderStatus.Created:
                            //支付定金
                            bllOrder.OrderFlow_PayDeposit(order);
                            break;
                        case Dianzhu.Model.Enums.enum_OrderStatus.Ended:
                            bllOrder.OrderFlow_CustomerPayFinalPayment(order);
                            break;
                        default:
                            break;
                    }

                    //调用IMServer,发送订单状态变更通知.

                    Response.Write("success");

                }
                else
                {
                    Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                }

                //打印页面
                Response.Write("验证成功<br />");
                Response.Write("trade_no=" + trade_no);
                log.Debug("6");
                //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else//验证失败
            {
                Response.Write("验证失败");
            }
        }
        else
        {
            Response.Write("无返回参数");
        }
    }

    private void BllOrder_OrderPayed(ServiceOrder order)
    {

    }

    /// <summary>
    /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestGet()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.QueryString;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
        }

        return sArray;
    }
}