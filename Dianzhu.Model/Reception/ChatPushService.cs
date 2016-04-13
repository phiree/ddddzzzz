using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{




    /// <summary>
    /// 推送的服务消息,供用户选择
    /// </summary>
    public class ReceptionChatPushService : ReceptionChat
    {
        public ReceptionChatPushService()
        {
            this.ChatType = enum_ChatType.PushedService;
        }
        public virtual IList<ServiceOrderPushedService> PushedServices { get; set; }
    }


}
