using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
 
    /// <summary>
    /// 接待中的聊天记录.
    /// </summary>
    public class ReceptionChat
    {
        public ReceptionChat()
        {
            ChatType = Enums.enum_ChatType.Text;
            SavedTime = DateTime.Now;
        }
        public static ReceptionChat Create(Enums.enum_ChatType chatType)
        {
            ReceptionChat chat;
            switch (chatType)
            {
               
                case Enums.enum_ChatType.Media:
                    chat = new ReceptionChatMedia();
                    break;
                case Enums.enum_ChatType.Notice:
                    chat = new ReceptionChatNotice();
                    break;
                case Enums.enum_ChatType.UserStatus:
                    chat = new ReceptionChatUserStatus();
                    break;
                default:
                    chat= new ReceptionChat();
                    break;
            }
            chat.ChatType = chatType;
            return chat;
        }
        public virtual Guid Id { get; set; }
     
        //保存的时间, 作为排序依据.
        public virtual DateTime SavedTime { get; set; }
        public virtual DateTime SendTime { get; set; }//发送时间
        public virtual DateTime ReceiveTime { get; set; }//接收时间
        public virtual DZMembership From { get; set; }//发送方
        public virtual DZMembership To { get; set; }//接收方
        public virtual string MessageBody { get; set; }//消息的内容
        public virtual Enums.enum_ChatType ChatType { get; set; }
        public virtual ServiceOrder ServiceOrder { get; set;}
        public virtual int Version { get; set; }//版本号
        public virtual Enums.enum_ChatTarget ChatTarget { get; set; } //聊天状态，接待方是平台客服还是商家客服
        public virtual enum_XmppResource FromResource { get; set; }//from 的资源名
        public virtual enum_XmppResource ToResource { get; set; }//to 的资源名
        /// <summary>
        /// 消息中媒体文件的地址,多个媒体文件用分号风格.
        /// </summary>

        /// <summary>
        /// 消息中的服务信息
        /// </summary>
        /// <returns></returns>
        public virtual string BuildLine()
        {
            return SavedTime.ToShortTimeString() + " " + From.UserName + ":    " + MessageBody;
        }
        public virtual string BuildLine(DZMembership from)
        {
            if (from == this.From)
                return MessageBody + "   " + From.UserName + " " + SendTime.ToString("yyyy-MM-dd HH:mm:ss");
            else
            {
                return BuildLine();
            }
        }


    }

    /// <summary>
    /// 记录点点的接待记录
   
}
