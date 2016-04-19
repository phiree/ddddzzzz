using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
