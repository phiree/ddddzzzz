using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Specification;
using IdentityAccess.DomainModel;

namespace IdentityAccess.Infrastructure
{
    public class MembershipRepository : Common.Repository.IRepository<DomainModel.Membership>
    {
        public void Add(Membership entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Membership> Find(ISpecification<Membership> spec)
        {
            throw new NotImplementedException();
        }

        public Membership FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Membership FindOne(ISpecification<Membership> spec)
        {
            throw new NotImplementedException();
        }

        public void Remove(Membership entity)
        {
            throw new NotImplementedException();
        }
    }
}
