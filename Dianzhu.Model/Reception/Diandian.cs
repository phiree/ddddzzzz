﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
   public class Diandian:CustomerService
    {
        protected Diandian() { }
        public Diandian(string memberId, string name):base(memberId,"点点")
        {
            
        }
        public virtual string MemberId { get; set; }
        
    }
}
