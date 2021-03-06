﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
namespace Dianzhu.DAL
{
    public class DALStaff : DALBase<Staff>
    {
        public DALStaff()
        {
           
        }
        //注入依赖,供测试使用;
        public DALStaff(string fortest):base(fortest)
        {
            
        }
        public IList<Staff> GetList(Guid businessId, Guid serviceTypeId, int pageindex, int pagesize, out int totalRecord)
        {
            string where = "s.Belongto.Id='" + businessId + "'";
            if (serviceTypeId != Guid.Empty && serviceTypeId != null)
            {
                where += " and s.ServiceType.Id='" + serviceTypeId + "'";
            }
            string total_query = "select count(*) from Staff s where " + where;
            return GetList("select s from Staff s where " + where,"s.Code",true, pageindex, pagesize, out totalRecord,total_query);
        }

        public int GetEnableSum(Business business)
        {
            return Session.QueryOver<Staff>().Where(x => x.Belongto == business).And(x => x.Enable == true).RowCount();
        }

        public IList<Staff> GetAllListByBusiness(Business business)
        {
            return Session.QueryOver<Staff>().Where(x => x.Belongto == business).List();
        }
    }
}
