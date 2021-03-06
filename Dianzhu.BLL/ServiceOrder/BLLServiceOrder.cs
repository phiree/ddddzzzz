﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

using Dianzhu.DAL;
using Dianzhu.Model.Enums;
using Dianzhu.Pay;

namespace Dianzhu.BLL
{

    /// <summary>
    /// 订单业务逻辑
    /// </summary>
    public class BLLServiceOrder
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        DALServiceOrder DALServiceOrder = null;
        DZMembershipProvider membershipProvider = null;

        BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis = null;

        public BLLServiceOrder(DALServiceOrder dalServiceOrder, BLLServiceOrderStateChangeHis bllServiceOrderStateChangeHis, DZMembershipProvider membershipProvider)
        {
            this.DALServiceOrder = dalServiceOrder;
            this.bllServiceOrderStateChangeHis = bllServiceOrderStateChangeHis;
            this.membershipProvider = membershipProvider;
        }

        public BLLServiceOrder() : this(new DALServiceOrder(), new BLLServiceOrderStateChangeHis(), new DZMembershipProvider())
        {
        }



        public BLLServiceOrder(DALServiceOrder dal)
        {
            DALServiceOrder = dal;
        }


        #region 基本操作

        public IList<ServiceOrder> GetListForBusiness(object b)
        {
            throw new NotImplementedException();
        }

        public int GetServiceOrderCount(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType)
        {
            return DALServiceOrder.GetServiceOrderCount(userId, searchType);
        }
        public IList<ServiceOrder> GetServiceOrderList(Guid userId, Dianzhu.Model.Enums.enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            return DALServiceOrder.GetServiceOrderList(userId, searchType, pageNum, pageSize);
        }

        public virtual ServiceOrder GetOne(Guid guid)
        {
            return DALServiceOrder.GetOne(guid);
        }
        public void SaveOrUpdate(ServiceOrder order)
        {
            order.LatestOrderUpdated = DateTime.Now;
            DALServiceOrder.SaveOrUpdate(order);
        }
        public IList<ServiceOrder> GetAll() //获取全部订单
        {
            return DALServiceOrder.GetAll<ServiceOrder>();
        }

        public IList<ServiceOrder> GetAllByOrderStatus(Dianzhu.Model.Enums.enum_OrderStatus status)
        {
            return DALServiceOrder
               .GetAll<ServiceOrder>()
               .Where(x => x.OrderStatus == status)
               .ToList();
        }

        



        public IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        {
            return DALServiceOrder.GetListForBusiness(business, pageNum, pageSize, out totalAmount);
        }

        public IList<ServiceOrder> GetListForCustomer(DZMembership customer, int pageNum, int pageSize, out int totalAmount)
        {
            return DALServiceOrder.GetListForCustomer(customer, pageNum, pageSize, out totalAmount);
        }

        public void Delete(ServiceOrder order)
        {
            DALServiceOrder.Delete(order);
        }

        public virtual ServiceOrder GetDraftOrder(DZMembership c, DZMembership cs)
        {
            return DALServiceOrder.GetDraftOrder(c, cs);
        }
        public IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime date)
        {
            return DALServiceOrder.GetOrderListByDate(service, date);
        }
        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, DZMembership customer)
        {
            return DALServiceOrder.GetOrderByIdAndCustomer(Id, customer);
        }
        #endregion

        #region 订单流程变化

        /// <summary>
        /// 用户定金支付完成,等待后台确认订单是否到帐
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_PayDepositAndWaiting(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.CheckPayWithDesposit);
        }

        /// <summary>
        /// 后台确认订单到帐
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ConfirmDeposit(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Payed);
        }
        /// <summary>
        /// 商家确认订单,准备执行.
        /// </summary>
        public void OrderFlow_BusinessConfirm(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Negotiate);
        }
        /// <summary>
        /// 商家输入协议
        /// </summary>
        /// <param name="order"></param>
        /// <param name="negotiateAmount"></param>
        public void OrderFlow_BusinessNegotiate(ServiceOrder order, decimal negotiateAmount)
        {
            order.NegotiateAmount = negotiateAmount;
            if (negotiateAmount < order.DepositAmount)
            {
                log.Warn("协商价格小于订金");
            }

            ChangeStatus(order, enum_OrderStatus.isNegotiate);
        }
        /// <summary>
        /// 商户已经提交新价格，等待用户确认
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomIsNegotiate(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Assigned);
        }
        /// <summary>
        /// 用户确认协商价格,并确定开始服务
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerConfirmNegotiate(ServiceOrder order)
        {
            order.OrderServerStartTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Begin);
        }
        /// <summary>
        /// 商家确定服务完成
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.IsEnd);
        }
        /// <summary>
        /// 用户确认服务完成。
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerFinish(ServiceOrder order)
        {
            order.OrderServerFinishedTime = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Ended);
        }
        /// <summary>
        /// 用户支付尾款
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayFinalPayment(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.CheckPayWithNegotiate);
        }

        /// <summary>
        /// 订单完成
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_OrderFinished(ServiceOrder order)
        {
            order.OrderFinished = DateTime.Now;
            ChangeStatus(order, enum_OrderStatus.Finished);
        }

        /// <summary>
        /// 订单评价
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerAppraise(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Appraised);
        }

        /// <summary>
        /// 用户申请理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Refund);
            if (true)//todo：是否在质保期，质保期？？
            {
                ChangeStatus(order, enum_OrderStatus.WaitingRefund);
            }
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessIsRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.isRefund);
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_RefundSuccess(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.EndRefund);
        }

        /// <summary>
        /// 商户要求支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessAskPayWithRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.AskPayWithRefund);
        }

        /// <summary>
        /// 商户驳回理赔请求
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_BusinessRejectRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.RejectRefund);
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_WaitingPayWithRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.WaitingPayWithRefund);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.CheckPayWithRefund);
        }

        /// <summary>
        /// 一点办官方介入
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBInsertIntervention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.InsertIntervention);
        }

        /// <summary>
        /// 等待一点办官方处理
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBHandleWithIntervention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.HandleWithIntervention);
        }

        /// <summary>
        /// 一点办已确认理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_YDBConfirmNeedRefund(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.NeedRefundWithIntervention);
            //todo:等待退还金额
        }

        /// <summary>
        /// 一点办要求用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_NeedPayInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.NeedPayWithIntervention);
        }

        /// <summary>
        /// 用户支付赔偿金
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_CustomerPayInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.CheckPayWithIntervention);
        }

        /// <summary>
        /// 理赔成功
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ConfirmInternention(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.EndIntervention);
        }

        /// <summary>
        /// 商户裁定理赔
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_ForceStop(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.ForceStop);
        }

        //订单状态改变通用方法
        private void ChangeStatus(ServiceOrder order, enum_OrderStatus targetStatus)
        {
            enum_OrderStatus oldStatus = order.OrderStatus;

            OrderServiceFlow flow = new OrderServiceFlow();
            flow.ChangeStatus(order, targetStatus);

            //保存订单历史记录
            order.OrderStatus = oldStatus;
            bllServiceOrderStateChangeHis.SaveOrUpdate(order, targetStatus);

            //更新订单状态
            order.OrderStatus = targetStatus;
            SaveOrUpdate(order);

            log.Debug("调用IMServer,发送订单状态变更通知");
            System.Net.WebClient wc = new System.Net.WebClient();
            string notifyServer = Dianzhu.Config.Config.GetAppSetting("NotifyServer");
            Uri uri = new Uri(notifyServer + "type=ordernotice&orderId=" + order.Id);
            System.IO.Stream returnData = wc.OpenRead(uri);
            System.IO.StreamReader reader = new System.IO.StreamReader(returnData);
            string result = reader.ReadToEnd();
            log.Debug("发送结果:" + result);
        }
        #endregion

        #region 订单取消
        /// <summary>
        /// 申请取消
        /// </summary>
        /// <param name="order"></param>
        public void OrderFlow_Canceled(ServiceOrder order)
        {
            ChangeStatus(order, enum_OrderStatus.Canceled);

            switch (order.OrderStatus)
            {
                case enum_OrderStatus.Created:
                    ChangeStatus(order, enum_OrderStatus.EndCancel);
                    break;
                case enum_OrderStatus.Payed:
                case enum_OrderStatus.Negotiate:
                case enum_OrderStatus.isNegotiate:
                case enum_OrderStatus.Assigned:

                    ////获取确认时间
                    //var negotiateTime = bllServiceOrderStateChangeHis.GetChangeTime(order, enum_OrderStatus.Negotiate);
                    var targetTime = order.Details[0].TargetTime;
                    if (DateTime.Now <= targetTime)
                    {
                        double timeSpan = (DateTime.Now - targetTime).TotalMinutes;
                        //整个取消
                        if (order.ServiceOvertimeForCancel <= timeSpan)
                        {
                            //todo:退还定金
                            ChangeStatus(order, enum_OrderStatus.WaitingDepositWithCanceled);
                        }
                        else {
                            //扣除定金，取消成功
                            ChangeStatus(order, enum_OrderStatus.EndCancel);
                        }
                    }
                    else
                    {
                        //扣除定金，取消成功
                        ChangeStatus(order, enum_OrderStatus.EndCancel);
                    }

                    break;


                default: break;
            }
        }
        #endregion

        #region 分配工作人员
        public void AssignStaff(ServiceOrder order, Staff staff)
        {
            BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();
            OrderAssignment oa = new OrderAssignment();
            oa.Order = order;
            oa.AssignedStaff = staff;

            bllOrderAssignment.SaveOrUpdate(oa);
        }
        public void DeassignStaff(ServiceOrder order, Staff staff)
        {
            BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();
            OrderAssignment oa = bllOrderAssignment.FindByOrderAndStaff(order, staff);
            oa.DeAssignedTime = DateTime.Now;
            oa.Enabled = false;

            bllOrderAssignment.SaveOrUpdate(oa);
        }
        #endregion

        #region http接口方法

        

        #endregion

    }
    /// <summary>
    /// 订单状态变更控制.
    /// </summary>
    public class OrderServiceFlow
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL");

        /// <summary>
        /// 确保目标状态是可以执行的.
        /// </summary>

        public void ChangeStatus(ServiceOrder order, Model.Enums.enum_OrderStatus targetStatus)
        {

            bool validated = dictAvailabelStatus[targetStatus].Contains(order.OrderStatus);
            if (validated)
            {
                order.OrderStatus = targetStatus;
            }
            else
            {
                string errMsg = string.Format("不合法的状态变更{0}->{1}", order.OrderStatus, targetStatus);
                log.Error(errMsg);
                throw new Exception(errMsg);
            }

        }

        //状态对应表. key:状态, value:该状态可以从哪些状态转变而来.
        static Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> dictAvailabelStatus =
            new Dictionary<enum_OrderStatus, IList<enum_OrderStatus>> {
                //正常支付流程订单状态变更
                { enum_OrderStatus.Created,new List<enum_OrderStatus>() {enum_OrderStatus.DraftPushed }},
                { enum_OrderStatus.CheckPayWithDesposit,new List<enum_OrderStatus>() {enum_OrderStatus.Created}},
                { enum_OrderStatus.Payed,new List<enum_OrderStatus>() {enum_OrderStatus.DraftPushed ,
                                                                        enum_OrderStatus.CheckPayWithDesposit}},
                { enum_OrderStatus.Negotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Payed,
                                                                            enum_OrderStatus.IsEnd }},
                { enum_OrderStatus.isNegotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Negotiate }},
                { enum_OrderStatus.Assigned,new List<enum_OrderStatus>() {enum_OrderStatus.isNegotiate }},
                { enum_OrderStatus.Begin,new List<enum_OrderStatus>() {enum_OrderStatus.Assigned }},
                { enum_OrderStatus.IsEnd,new List<enum_OrderStatus>() {enum_OrderStatus.Begin }},
                { enum_OrderStatus.Ended,new List<enum_OrderStatus>() {enum_OrderStatus.IsEnd ,
                                                                        enum_OrderStatus.Begin}},
                { enum_OrderStatus.CheckPayWithNegotiate,new List<enum_OrderStatus>() {enum_OrderStatus.Ended }},
                { enum_OrderStatus.Finished,new List<enum_OrderStatus>() {enum_OrderStatus.CheckPayWithNegotiate }},
                { enum_OrderStatus.Appraised,new List<enum_OrderStatus>() {enum_OrderStatus.Finished }},
                { enum_OrderStatus.EndWarranty,new List<enum_OrderStatus>() {enum_OrderStatus.Appraised }},

                //订单取消状态可从哪些状态变更而来
                { enum_OrderStatus.Canceled,new List<enum_OrderStatus>() {enum_OrderStatus.Created,
                                                                            enum_OrderStatus.Payed,
                                                                             enum_OrderStatus.Negotiate,
                                                                              enum_OrderStatus.isNegotiate,
                                                                               enum_OrderStatus.Assigned}},
                //取消流程订单状态变更
               { enum_OrderStatus.WaitingDepositWithCanceled,new List<enum_OrderStatus>() {enum_OrderStatus.Canceled}},
               { enum_OrderStatus.EndCancel,new List<enum_OrderStatus>() { enum_OrderStatus.WaitingDepositWithCanceled,
                                                                            enum_OrderStatus.Canceled}},

               //订单理赔状态可从哪些状态变更而来
               { enum_OrderStatus.Refund,new List<enum_OrderStatus>() {enum_OrderStatus.Begin,
                                                                        enum_OrderStatus.IsEnd,
                                                                         enum_OrderStatus.Ended,
                                                                           enum_OrderStatus.Finished,
                                                                            enum_OrderStatus.Appraised}},

               //理赔流程订单状态变更
               { enum_OrderStatus.WaitingRefund,new List<enum_OrderStatus>() {enum_OrderStatus.Refund }},
               { enum_OrderStatus.isRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.RejectRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.AskPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingRefund }},
               { enum_OrderStatus.WaitingPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.AskPayWithRefund }},
               { enum_OrderStatus.CheckPayWithRefund,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingPayWithRefund }},
               { enum_OrderStatus.EndRefund,new List<enum_OrderStatus>() {enum_OrderStatus.isRefund,
                                                                            enum_OrderStatus.CheckPayWithRefund}},
               //订单一点办介入状态从哪个状态变更而来
               { enum_OrderStatus.InsertIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.RejectRefund,
                                                                                    enum_OrderStatus.AskPayWithRefund}},

               //介入流程订单状态变更
               { enum_OrderStatus.HandleWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.InsertIntervention }},
               { enum_OrderStatus.NeedRefundWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.HandleWithIntervention }},
               { enum_OrderStatus.NeedPayWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.HandleWithIntervention }},
               { enum_OrderStatus.CheckPayWithIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.NeedPayWithIntervention }},
               { enum_OrderStatus.EndIntervention,new List<enum_OrderStatus>() {enum_OrderStatus.NeedRefundWithIntervention,
                                                                                    enum_OrderStatus.CheckPayWithIntervention}},
               //投诉流程订单状态变更
               { enum_OrderStatus.WaitingComplaints,new List<enum_OrderStatus>() {enum_OrderStatus.Complaints }},
               { enum_OrderStatus.EndComplaints,new List<enum_OrderStatus>() {enum_OrderStatus.WaitingComplaints }},

               //强制终止状态的变更
               { enum_OrderStatus.ForceStop,new List<enum_OrderStatus>() {enum_OrderStatus.Ended,
                                                                          enum_OrderStatus.WaitingPayWithRefund,
                                                                           enum_OrderStatus.NeedPayWithIntervention}},
        };
    }

    /// <summary>
    /// 订单状态历史记录
    /// </summary>
    public class BLLServiceOrderStateChangeHis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLLServiceOrder");

        DALServiceOrderStateChangeHis dalServiceOrderStateChangeHis = null;
        public BLLServiceOrderStateChangeHis()
        {
            dalServiceOrderStateChangeHis = DALFactory.DALServiceOrderStateChangeHis;
        }

        public void SaveOrUpdate(ServiceOrder oldOrder,enum_OrderStatus newStatus)
        {
            int num = 1;
            ServiceOrderStateChangeHis oldOrderHis = GetMaxNumberOrderHis(oldOrder);
            if (oldOrderHis != null)
            {
                num = oldOrderHis.Number + 1;
            }
            ServiceOrderStateChangeHis orderHis = new ServiceOrderStateChangeHis(oldOrder, newStatus, num);
            dalServiceOrderStateChangeHis.SaveOrUpdate(orderHis);
        }

        public ServiceOrderStateChangeHis GetMaxNumberOrderHis(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetMaxNumberOrderHis(order);
        }

        public IList<ServiceOrderStateChangeHis> GetOrderHisList(ServiceOrder order)
        {
            return dalServiceOrderStateChangeHis.GetOrderHisList(order);
        }
        public DateTime GetChangeTime(ServiceOrder order, enum_OrderStatus status)
        {
            return dalServiceOrderStateChangeHis.GetChangeTime(order, status);
        }
    }

    public class BllServiceOrderAppraise
    {
        public DALServiceOrderAppraise dalServiceOrderAppraise = DALFactory.DALServiceOrderAppraise;
        
        public void Save(ServiceOrderAppraise appraise)
        {
            dalServiceOrderAppraise.Save(appraise);
        }
    }
}
