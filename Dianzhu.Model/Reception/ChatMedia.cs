using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
  
    /// <summary>
    /// 多媒体消息,
    ///
    public class ReceptionChatMedia : ReceptionChat
    {
       
        public virtual string MedialUrl { get; set; }
        public virtual string MediaType { get; set; }
    }
 
 
  

}
