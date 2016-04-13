using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 客户类
    /// </summary>
   public class Customer
    {
        /// <summary>
        ///ddd: accessidentity认证的id
        /// </summary>
        public string MemberId { get; protected set; }
        public string Name { get; set; }
        
    }
}
