using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAccess.OHS.Models
{
    public sealed class Response<TReturn> : Response
    {
        public TReturn Object { get; set; }
    }
}