﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class ServiceTypeMap : ClassMap<ServiceType>
    {
        public ServiceTypeMap()
        {
            Id(x => x.Code).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.DeepLevel);
            Map(x => x.OrderNumber);
            References<ServiceType>(x => x.Parent).Not.LazyLoad();
            HasMany<ServiceType>(x => x.Children).Cascade.All().Inverse().Not.LazyLoad();
            HasMany<ServiceProperty>(x => x.Properties).Not.LazyLoad();
            

        }
    
    }

}
