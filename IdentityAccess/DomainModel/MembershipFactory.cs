using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityAccess.DomainModel
{
   public  abstract class MembershipFactory
    {
        public void CreateAuthInfo()
        {

        }
        public abstract void Create(Membership membership);
    }
   
     
}
