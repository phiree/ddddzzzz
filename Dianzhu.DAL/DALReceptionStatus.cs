using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    /// <summary>
    ///
    /// </summary>
    public class DALReceptionStatus : DALBase<Model.ReceptionStatus>
    {
         public DALReceptionStatus()
        {

        }
        //注入依赖,供测试使用;
        public  DALReceptionStatus(string fortestonly): base(fortestonly)
         { }

         public virtual IList<ReceptionStatus> GetListByCustomerService(CustomerService customerService)
         {
           return   Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService == customerService).List();
         }
        public virtual CustomerService GetListByCustomerServiceId(Guid csid)
        {
            IList<ReceptionStatus> rsList = Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService.MemberId == csid.ToString()).Take(1).List();
            if (rsList.Count > 0)
            {
                return rsList.Select(x => x.CustomerService).First();
            }
            else
            {
                return null;
            }            
        }
        public virtual IList<ReceptionStatus> GetListByCustomer(Customer customer)
         {
             return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer.MemberId == customer.MemberId).List();
      
         }
        public virtual ReceptionStatus GetOneByCustomerAndCS(CustomerService customerService, Customer customer)
        {

            ReceptionStatus result = null;
            var resultList = Session.QueryOver<ReceptionStatus>().Where(x => x.Customer == customer)
                .And(x => x.CustomerService == customerService).List();
            if (resultList.Count >= 1)
            {
                result = resultList[0];
            }
            else
            { result = null; }
            return result;
        }

        public virtual void DeleteAllCustomerAssign(Customer customer)
        {
            IList<ReceptionStatus> rsList = GetListByCustomer(customer);
            foreach (ReceptionStatus rs in rsList)
            {
                Session.Delete(rs);
            }
        }
        public virtual void DeleteAllCustomerServiceAssign(CustomerService customerService)
        {
            IList<ReceptionStatus> rsList = GetListByCustomerService(customerService);
            foreach (ReceptionStatus rs in rsList)
            {
                Session.Delete(rs);
            }
        }

        public virtual IList<CustomerService> GetCSMinCount(Diandian diandian)
        {
            var result = Session.QueryOver<ReceptionStatus>().Select(
                Projections.Group<ReceptionStatus>(e => e.CustomerService),
                Projections.Count<ReceptionStatus>(e => e.CustomerService)).
                Where(e => e.CustomerService.MemberId != diandian.MemberId).
                OrderBy(Projections.Count<ReceptionStatus>(e => e.CustomerService)).Asc.List<object[]>();

            IList<CustomerService> dzList = new List<CustomerService>();
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    dzList.Add((CustomerService)result[i][0]);
                }
            }            
          
            return dzList;
        }

        public virtual IList<ReceptionStatus> GetRSListByDiandian(Diandian diandian,int num)
        {
            return Session.QueryOver<ReceptionStatus>().Where(x => x.CustomerService.MemberId == diandian.MemberId).OrderBy(x => x.LastUpdateTime).Asc.Take(num).List();
        }

        public virtual ReceptionStatus GetOrder(Customer c,CustomerService cs)
        {
            return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer == c).And(x => x.CustomerService == cs).List()[0];
        }

        public virtual ReceptionStatus GetOneByCustomer(Guid customerId)
        {
            return Session.QueryOver<ReceptionStatus>().Where(x => x.Customer.MemberId == customerId.ToString()).SingleOrDefault();
        }
    }
}
