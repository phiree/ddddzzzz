using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.DAL
{
    /// <summary>
    /// ddd:repository implement .
    /// </summary>
   public class DALBusinessUser:DALBase<BusinessUser>
    {
        public Dianzhu.Model.BusinessUser Get(string memberId)
        {
           return Session.Get<BusinessUser>(memberId);
        }
    }
}
