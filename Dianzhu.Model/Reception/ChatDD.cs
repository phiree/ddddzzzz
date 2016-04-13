using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
 
    

    /// <summary>
    /// 记录点点的接待记录
    /// </summary>
    public class ReceptionChatDD
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime SavedTime { get; set; }//保存的时间, 作为排序依据.
        public virtual DateTime SendTime { get; set; }//发送时间
        public virtual DateTime ReceiveTime { get; set; }//接收时间
        public virtual DZMembership From { get; set; }//发送方
        public virtual DZMembership To { get; set; }//接收方
        public virtual string MessageBody { get; set; }//消息的内容
        public virtual Enums.enum_ChatType ChatType { get; set; }
        public virtual ServiceOrder ServiceOrder { get; set; }
        public virtual int Version { get; set; }//版本号
        public virtual bool IsCopy { get; set; }//是否复制过该聊天记录
        public virtual string MedialUrl { get; set; }
        public virtual string MediaType { get; set; }
        public virtual Enums.enum_ChatTarget ChatTarget { get; set; }//聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; set; }//from 的资源名,to 都是发给点点的，忽略不存
    }

     
}
