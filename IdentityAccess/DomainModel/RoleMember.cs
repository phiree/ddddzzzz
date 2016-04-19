using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityAccess.DomainModel
{
    /// <summary>
    /// 用户和角色的对应关系
    /// </summary>
   public  class RoleMember
    {
        public virtual Guid Id { get; set; }
        public virtual Membership Member { get; set; }
        public virtual DZRole Role { get; set; }

        
    }
}
