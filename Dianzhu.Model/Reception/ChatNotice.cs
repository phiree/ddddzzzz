using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{  
    /// <summary>
    /// 
    /// </summary>
    public class ReceptionChatNotice : ReceptionChat
    {
        public virtual DZMembership UserObj { get; set; }
    }
}
 
