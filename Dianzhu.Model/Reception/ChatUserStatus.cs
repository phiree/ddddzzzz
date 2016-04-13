using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 用户状态改变的消息
    /// </summary>
    public class ReceptionChatUserStatus : ReceptionChat
    {
        public virtual DZMembership User { get; set; }//状态发生变化的用户
        public virtual enum_UserStatus Status { get; set; }//用户状态
    }

}
