using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
/// <summary>
/// ddd:application service
/// </summary>
namespace Dianzhu.BLL
{
   public  class CustomerServiceService
    {
        
        public CustomerServiceService()
        {

        }
        /// <summary>
        /// 客服注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="realname"></param>
        /// <returns></returns>
        public CustomerService Register(string userName,string password,string email,string phone,string realname)
        {
            DZMembership member =   DZMembership.Create(userName, password, email, phone);

            throw new NotImplementedException();


        }
    }
}
