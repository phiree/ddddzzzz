using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model { 
    /// <summary>
    /// 商户帐号
    /// </summary>
   public class BusinessUser
    {
        /// <summary>
        /// 创建一个商户帐号
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public static BusinessUser CreateBusinessUser(string memberId)
        {
            BusinessUser bu = new BusinessUser();
            bu.MemberId = memberId;
            return bu;
        }
        /// <summary>
        /// 会员id,来自IA
        /// </summary>
        public string MemberId { get; protected set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }
        
    }
}
