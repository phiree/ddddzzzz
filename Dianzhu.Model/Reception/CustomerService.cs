using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public class CustomerService
    {
        protected CustomerService() { }
        public CustomerService(string memberId,string name)
        {
            this.MemberId = memberId;
            this.Name = name;
        }
        public virtual string MemberId { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Avatar { get; protected set; }
        public virtual void ChangeAvatar()
        { }
    }
}
