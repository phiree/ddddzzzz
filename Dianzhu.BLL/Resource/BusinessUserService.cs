using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using System.Web.Security;
namespace Dianzhu.BLL.Resource
{
   public class BusinessUserService
    {
        //ddd:集成限界上下文.
        DZMembershipProvider memberService;
        DAL.DALBusinessUser dalBusinessUser;

        public BusinessUserService(DZMembershipProvider memberService):this(memberService,new DAL.DALBusinessUser()) { }
        public BusinessUserService(DZMembershipProvider memberService,DAL.DALBusinessUser dalBusinessUser)
        {
            this.memberService = memberService;
            this.dalBusinessUser = dalBusinessUser;
        }
        public Dianzhu.Model.BusinessUser Create(string userName, string userPhone, string userEmail, string password,
            out MembershipCreateStatus createStatus, string userType)
        {
            createStatus = MembershipCreateStatus.Success;
            DZMembership member= memberService.CreateUser(userName, userPhone, userEmail, password, out createStatus, userType);
            Dianzhu.Model.BusinessUser bu = Dianzhu.Model.BusinessUser.CreateBusinessUser(member.Id.ToString());
            return bu;
        }
        public BusinessUser GetOne(string memberId)
        {
            return dalBusinessUser.Get(memberId);
        }
    }
}
