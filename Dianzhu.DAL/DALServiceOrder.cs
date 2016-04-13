﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using NHibernate;
using NHibernate.Criterion;
namespace Dianzhu.DAL
{
    public class DALServiceOrder : DALBase<ServiceOrder>
    {
        public DALServiceOrder()
        {

        }
        //注入依赖,供测试使用;
        public DALServiceOrder(string fortest) : base(fortest)
        {

        }
        public IList<ServiceOrder> GetListByUser(Guid userId)
        {
            var iqueryover = GetList(userId, enum_OrderSearchType.ALL);
            return iqueryover.List();
        }
        private IQueryOver<ServiceOrder> GetList(Guid userId, enum_OrderSearchType searchType)
        {
            IQueryOver<ServiceOrder, ServiceOrder> iqueryover = Session.QueryOver<ServiceOrder>().Where(x => x.Customer.MemberId == userId.ToString());

            switch (searchType)
            {

                case enum_OrderSearchType.De:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus == enum_OrderStatus.Finished
                        && x.OrderStatus == enum_OrderStatus.Aborded
                        && x.OrderStatus == enum_OrderStatus.Appraised
                        );
                    break;
                case enum_OrderSearchType.Nt:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Finished
                         && x.OrderStatus != enum_OrderStatus.Aborded
                         && x.OrderStatus != enum_OrderStatus.Appraised
                         && x.OrderStatus != enum_OrderStatus.Search
                    );
                    break;
                default:
                case enum_OrderSearchType.ALL:
                    iqueryover = iqueryover.Where(
                        x => x.OrderStatus != enum_OrderStatus.Draft
                         && x.OrderStatus != enum_OrderStatus.DraftPushed
                         && x.OrderStatus != enum_OrderStatus.Search
                        );
                    break;

            }
            iqueryover = iqueryover.OrderBy(x => x.OrderCreated).Desc;
            return iqueryover;
        }
        public int GetServiceOrderCount(Guid userId, enum_OrderSearchType searchType)
        {
            var iqueryover = GetList(userId, searchType);
            int rowCount = iqueryover.RowCount();
            return rowCount;
        }


        public IList<ServiceOrder> GetServiceOrderList(Guid userId, enum_OrderSearchType searchType, int pageNum, int pageSize)
        {
            var iqueryover = GetList(userId, searchType);
            var result = iqueryover.Skip((pageNum - 1) * pageSize).Take(pageSize).List();
            return result;
        }

        public IList<ServiceOrder> GetListForBusiness(Business business, int pageNum, int pageSize, out int totalAmount)
        {
            var iquery = Session.QueryOver<ServiceOrder>()
             //   .Where(a => a.Details.Select(x => x.OriginalService).Select(y => y.Business).Contains(business));
            ;
                //.JoinQueryOver(x => x.Service)
                //.JoinQueryOver(y => y.Business)
               
            totalAmount = iquery.RowCount();

            IList<ServiceOrder> list = iquery.List(). OrderByDescending(x=>x.LatestOrderUpdated).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            return list;
        }

        public IList<ServiceOrder> GetListForCustomer(Customer customer,int pageNum,int pageSize,out int totalAmount)
        {
            var iquery = Session.QueryOver<ServiceOrder>().Where(x => x.Customer == customer);
            totalAmount = iquery.RowCount();

            IList<ServiceOrder> list = iquery.OrderBy(x => x.OrderFinished).Desc.Skip((pageNum - 1) * pageSize).Take(pageSize).List();

            return list;
        }

        /// <summary>
        /// 获得草稿订单
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServiceOrder GetDraftOrder(Customer c, CustomerService cs)
        {
            var order = Session.QueryOver<ServiceOrder>().
                Where(x => x.Customer == c).
                And(x => x.CustomerService == cs).
                And(x => x.OrderStatus == enum_OrderStatus.Draft).List();

            if (order.Count > 0)
            {
                return (ServiceOrder)order[0];
            }
            else
            {
                return null;
            }
        }

        public IList<ServiceOrder> GetOrderListByDate(DZService service, DateTime dateTime)
        {
            var orderList = Session.QueryOver<ServiceOrder>()
                 .Where(x => x.Service == service)
                 .And(x => x.OrderCreated.Date == dateTime.Date)
                 .List();
            return orderList;
        }

        public ServiceOrder GetOrderByIdAndCustomer(Guid Id, Customer customer)
        {
            return Session.QueryOver<ServiceOrder>().Where(x => x.Id == Id).And(x => x.Customer == customer).SingleOrDefault();
        }
    }
}
