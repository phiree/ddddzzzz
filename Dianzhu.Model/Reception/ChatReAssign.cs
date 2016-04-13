using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{

    /// <summary>
    /// 重新分配客服的消息
    /// </summary>
    public class ReceptionChatReAssign : ReceptionChat
    {
        /// <summary>
        /// 重新分配的客服
        /// </summary>
        public virtual DZMembership ReAssignedCustomerService { get; set; }
    }
}


