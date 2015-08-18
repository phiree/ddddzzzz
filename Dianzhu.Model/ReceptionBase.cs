﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 接待记录基类, 记录一次接待情况
    /// </summary>
    public class ReceptionBase
    {
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 接待发起方
        /// </summary>
        public virtual DZMembership Sender { get; set; }
        /// <summary>
        /// 接收方
        /// </summary>
        public virtual DZMembership Receiver { get; set; }
        public virtual DateTime TimeBegin { get; set; }
        public virtual DateTime TimeEnd { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public virtual int SatisfactionRate { get; set; }
        /// <summary>
        /// 链条记录
        /// </summary>
        public virtual IList<ReceptionChatHistory> ChatHistory { get; set; }

        public virtual bool IsComplete { get; set; }

    }
    public class ReceptionCustomer : ReceptionBase
    {
        /// <summary>
        /// 推送的服务
        /// </summary>
        public virtual IList<DZService> PushedServices { get; set; }
        public virtual DZMembership TransferFrom { get; set; }
        /// <summary>
        /// 确认的订单
        /// </summary>
        public virtual IList<ServiceOrder> Order { get; set; }
         
    }

    public class ReceptionBusiness : ReceptionBase
    {
        /// <summary>
        /// 商家和 客户的聊天记录.
        /// 针对哪个订单的接待.
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
    }
    /// <summary>
    /// 接待中的聊天记录.
    /// </summary>
    public class ReceptionChatHistory
    {
        public DateTime SendTime { get; set; }
        public DateTime ReceiveTime { get; set; }
        public DZMembership From { get; set; }
        public DZMembership To { get; set; }
        public string MessageBody { get; set; }
       
         

    }
}
