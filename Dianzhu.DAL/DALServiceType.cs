﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALServiceType : IDAL.IDALServiceType
    {
        IDAL.IDALBase<ServiceType> dalBase = null;
        public IDAL.IDALBase<ServiceType> DalBase
        {
            get { return new DalBase<ServiceType>(); }
            set { dalBase = value; }
        }

        public IList<ServiceType> GetTopList()
        {
            string query = "select s from ServiceType s where s.Parent is null";
            return DalBase.GetList(query);
        }
         
        

         
    }
}