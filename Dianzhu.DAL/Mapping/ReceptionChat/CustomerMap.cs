using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
namespace Dianzhu.DAL.Mapping
{
   public  class CustomerMap:ClassMap<Dianzhu.Model.Customer>
    {
        public CustomerMap()
        {
            Id(x => x.MemberId);
            Map(x => x.Name);
        }
    }
}
