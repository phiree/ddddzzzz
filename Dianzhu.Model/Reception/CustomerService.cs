using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public class CustomerService
    {
        public CustomerService(string memberId,string name)
        {
            this.MemberId = memberId;
            this.Name = name;
        }
        public string MemberId { get; protected set; }
        public string Name { get; set; }
    }
}
