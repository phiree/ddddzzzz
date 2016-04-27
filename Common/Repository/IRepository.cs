using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Domain;
using Common.Specification;

namespace Common.Repository
{
    public interface IRepository<TEntity> 
        where TEntity : IDomainEntity
    {
        TEntity FindById(string id);
        TEntity FindOne(ISpecification<TEntity> spec);
        IEnumerable<TEntity> Find(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
