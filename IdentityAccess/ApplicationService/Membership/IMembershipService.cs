using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAccess.ApplicationService.Membership
{
  public   interface IMembershipService
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="membership"></param>
        /// <returns></returns>
        MembershipDto Register(MembershipDto membership);
        MembershipDto FindById(string id);
        bool ValidateUser(MembershipDto membership);
    }
}
