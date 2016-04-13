using System;
using System.Collections.Generic;
using System.Linq;
using Dianzhu.Model;
using NHibernate;
using Dianzhu.Model.Enums;

namespace Dianzhu.DAL
{
    public class DALReception : DALBase<Model.ReceptionChat>
    {
         public DALReception()
        {
             
        }
        //注入依赖,供测试使用;
         public DALReception(string fortest):base(fortest)
        {
            
        }
        

        public virtual IList<ReceptionChat> GetListTest()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="orderId"></param>
        /// <param name="timeBegin"></param>
        /// <param name="timeEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public virtual IList<ReceptionChat> GetReceptionChatList(string memberIdfrom,string memberIdto, Guid orderId, DateTime timeBegin, DateTime timeEnd,
            int pageIndex, int pageSize, enum_ChatTarget target, out int rowCount
            )
        {

            var result = BuildReceptionChatQuery(memberIdfrom, memberIdto, orderId, timeBegin, timeEnd);
            if(orderId!=Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }

            switch (target)
            {
                case enum_ChatTarget.cer:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.cer);
                    break;
                case enum_ChatTarget.store:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.store);
                    break;
            }
            result = result.And(x => x.ChatType != enum_ChatType.ReAssign).And(x => x.ChatType != enum_ChatType.Notice);
            rowCount = result.RowCount();
            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            if (pageIndex < 0 && pageSize < 0)
            {
                receptionChatList = result.OrderBy(x => x.SavedTime).Desc.List().OrderBy(x => x.SavedTime).ToList();
            }
            else
            {
                receptionChatList = result.OrderBy(x => x.SavedTime).Desc.Skip(pageIndex * pageSize).Take(pageSize).List().OrderBy(x => x.SavedTime).ToList();
            }            
            return receptionChatList;
        }

        public virtual IList<ReceptionChat> GetReceptionChatListByTargetIdAndSize(string memberIdfrom, string memberIdto, Guid orderId, DateTime timeBegin, DateTime timeEnd,
             int pageSize, ReceptionChat targetChat, string low, enum_ChatTarget target)
        {
            var result = Session.QueryOver<ReceptionChat>();
            switch (target)
            {
                case enum_ChatTarget.cer:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.cer);
                    break;
                case enum_ChatTarget.store:
                    result = result.And(x => x.ChatTarget == enum_ChatTarget.store);
                    break;
            }
            if (low == "Y")
            {
                result = result.Where(x => x.SavedTime < targetChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
            }
            else
            {
                result = result.Where(x => x.SavedTime > targetChat.SavedTime).OrderBy(x => x.SavedTime).Desc;
            }
            if (!string.IsNullOrEmpty(memberIdto))
            {
                result = result.And(x => (x.MemberIdFrom == memberIdfrom && x.MemberIdTo == memberIdto) 
                || (x.MemberIdFrom == memberIdto && x.MemberIdTo == memberIdfrom));
            }
            else
            {
                if (!string.IsNullOrEmpty(memberIdfrom))
                {
                    result = result.And(x => (x.MemberIdFrom == memberIdfrom || x.MemberIdTo == memberIdfrom));
                }
            }
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            
            IList<ReceptionChat> receptionChatList = new List<ReceptionChat>();
            receptionChatList = result.Take(pageSize).List().OrderBy(x => x.SavedTime).ToList();

            return receptionChatList;
        }

        private IQueryOver<ReceptionChat, ReceptionChat> BuildReceptionChatQuery(string  memberIdfrom,string memberIdto, Guid orderId, DateTime timeBegin, DateTime timeEnd)
        {
            var result = Session.QueryOver<ReceptionChat>().Where(x => x.SavedTime >= timeBegin)
                .And(x => x.SavedTime <= timeEnd);
            if (!string.IsNullOrEmpty(memberIdto) ){
                result= result.And(x => (x.MemberIdFrom == memberIdfrom && x.MemberIdTo == memberIdto) 
                || (x.MemberIdFrom == memberIdto && x.MemberIdTo == memberIdfrom));
            }
            else
            {
                if(memberIdfrom!=null)
                {  
                result = result.And(x => (x.MemberIdFrom == memberIdfrom || x.MemberIdTo ==memberIdfrom));
                }
            }
            if (orderId != Guid.Empty)
            {
                result = result.And(x => x.ServiceOrder.Id == orderId);
            }
            return result;
        }
      
    }
}
