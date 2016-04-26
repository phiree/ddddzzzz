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

        public void Register(MembershipDto membershipDto)
        {
           // Domain.Membership.Create(membershipDto.)
        }

        
    }
}
