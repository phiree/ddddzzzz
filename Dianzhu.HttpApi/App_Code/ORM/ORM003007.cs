﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM003007 : BaseResponse
{
    public ResponseORM003007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

        ReqDataORM003007 requestData = this.request.ReqData.ToObject<ReqDataORM003007>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string user_id = requestData.userID;
        string order_id = requestData.orderID;

        try
        {
            Guid userId, orderId;
            bool isUserId = Guid.TryParse(user_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                }
            }
            else
            {
                member = p.GetUserById(userId);
            }
            try
            {
                enum_OrderStatus status = enum_OrderStatus.Unknow;
                try
                {
                    status = (enum_OrderStatus)Enum.Parse(typeof(enum_OrderStatus), requestData.status);
                }
                catch (Exception e)
                {
                    PHSuit.ExceptionLoger.ExceptionLog(ilog, e);
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "要变更的状态无效!";
                    return;
                }
                
                try
                {
                    OrderServiceFlow osf = new OrderServiceFlow();
                    if (member.UserType.ToLower() == "customer")
                    {
                        ServiceOrder order = bllServiceOrder.GetOrderByIdAndCustomer(orderId, member);
                        if (order == null)
                        {
                            this.state_CODE = Dicts.StateCode[4];
                            this.err_Msg = "没有对应的订单!";
                            return;
                        }

                        switch (status)
                        {
                            case enum_OrderStatus.CheckPayWithDesposit:
                                bllServiceOrder.OrderFlow_PayDepositAndWaiting(order);
                                break;
                            case enum_OrderStatus.Assigned:
                                bllServiceOrder.OrderFlow_CustomIsNegotiate(order);
                                break;
                            case enum_OrderStatus.Canceled:
                                bllServiceOrder.OrderFlow_Canceled(order);
                                break;
                            case enum_OrderStatus.Ended:
                                bllServiceOrder.OrderFlow_CustomerFinish(order);
                                break;
                            case enum_OrderStatus.CheckPayWithNegotiate:
                                bllServiceOrder.OrderFlow_CustomerPayFinalPayment(order);
                                break;
                            case enum_OrderStatus.CheckPayWithRefund:
                                bllServiceOrder.OrderFlow_CustomerPayRefund(order);
                                break;
                            case enum_OrderStatus.CheckPayWithIntervention:
                                bllServiceOrder.OrderFlow_CustomerPayInternention(order);
                                break;

                            default:
                                ilog.Debug("用户Id：" + userId + "，用户类型："+member.UserType+"，禁止提交status为" + status+"的访问数据！");
                                this.state_CODE = Dicts.StateCode[1];
                                this.err_Msg = "用户提交的类型有误！";
                                return;
                        }
                    }
                    else if(member.UserType.ToLower() == "business")
                    {
                        ServiceOrder order = bllServiceOrder.GetOne(orderId);
                        if (order.Details[0].OriginalService.Business.Owner.Id != userId)
                        {
                            this.state_CODE = Dicts.StateCode[4];
                            this.err_Msg = "没有对应的订单!";
                            return;
                        }

                        switch (status)
                        {
                            case enum_OrderStatus.Negotiate:
                                bllServiceOrder.OrderFlow_BusinessConfirm(order);
                                break;
                            case enum_OrderStatus.Begin:
                                bllServiceOrder.OrderFlow_CustomerConfirmNegotiate(order);
                                break;
                            case enum_OrderStatus.IsEnd:
                                bllServiceOrder.OrderFlow_BusinessFinish(order);
                                break;

                            default:
                                ilog.Debug("用户Id：" + userId + "，用户类型：" + member.UserType + "，禁止提交status为" + status + "的访问数据！");
                                this.state_CODE = Dicts.StateCode[1];
                                this.err_Msg = "用户提交的类型有误!";
                                return;
                        }
                    }
                    else
                    {
                        ilog.Debug("该用户没有权限访问接口!用户Id：" + userId + ";用户类型：" + member.UserType);
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "该用户没有权限访问接口!";
                        return;
                    }
                }
                catch (Exception e)
                {
                    PHSuit.ExceptionLoger.ExceptionLog(ilog, e);
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = e.Message;
                    return;
                }

                this.state_CODE = Dicts.StateCode[0];
            }
            catch (Exception ex)
            {
                PHSuit.ExceptionLoger.ExceptionLog(ilog,ex);
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            PHSuit.ExceptionLoger.ExceptionLog(ilog, e);
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }
    }
}


