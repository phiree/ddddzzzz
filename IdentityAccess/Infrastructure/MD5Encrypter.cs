using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAccess.Infrastructure
{
    public class MD5Encrypter : DomainService.IEncryptService
    {
        public string Encrypt(string old)
        {
            return string.Join("",
            new MD5CryptoServiceProvider().ComputeHash(
            new MemoryStream(Encoding.UTF8.GetBytes(old))).Select(x => x.ToString("X2")));
        }
    }
}
