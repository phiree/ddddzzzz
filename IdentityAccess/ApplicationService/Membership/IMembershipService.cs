using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAccess.ApplicationService.Membership
{
  public   interface IMembershipService
    {
        void Register(MembershipDto membership);
    }
}
