using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAccess.DomainService
{
    public interface IEncryptService
    {
        string Encrypt(string old);
    }
}
