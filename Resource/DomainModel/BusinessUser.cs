using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.DomainModel
{
    public class BusinessUser 
    {
        public string IdentityId { get; protected set; }
        public string RealName { get; protected set; }

        public BusinessUser(string identityId, string realName)
        {
            this.IdentityId = identityId;
            this.RealName = realName;
        }
         
    }
}
