using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public  class CustomerServiceMap : ClassMap<Dianzhu.Model.CustomerService>
    {
        public CustomerServiceMap()
        {
            Id(x => x.MemberId);
            Map(x => x.Name);
        }
    }
}
