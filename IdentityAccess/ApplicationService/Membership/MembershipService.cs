using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAccess.ApplicationService.Membership;
using Common.Repository;
using Domain=IdentityAccess.DomainModel;
namespace IdentityAccess.ApplicationService.Membership
{
   public  class MembershipService:IMembershipService
    {
        IRepository<DomainModel.Membership> repoMembership;
        
        public MembershipService(IRepository<DomainModel.Membership> repoMembership)
        {
            this.repoMembership = repoMembership;
        }

        public MembershipDto Register(MembershipDto membershipDto)
        {
           Domain.Membership member= Domain.Membership.Create(membershipDto.UserName, "pwd", "email", "phone");
            membershipDto.Id = "12";
            return membershipDto;

        }
        public MembershipDto FindById(string id)
        {
          Domain.Membership member=  repoMembership.FindById(id);
            MembershipDto dto = new MembershipDto { UserName = member.UserName };
            return dto;
        }

        
    }
}
