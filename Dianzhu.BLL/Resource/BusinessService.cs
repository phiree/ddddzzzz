using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL
{
   public  class BusinessService
    {
        DAL.DALBusinessUser dalBusinessUser;
        public BusinessService(DAL.DALBusinessUser dalBusinessUser)
        {
            this.dalBusinessUser = dalBusinessUser;
        }
        /// <summary>
        /// 创建一个店铺
        /// </summary>
        /// <returns></returns>
        public Model.Business_Abs Create(string type,string businessUserId)
        {
           var bu= dalBusinessUser.Get(businessUserId);
          return   Model.Business_Abs.Create(type, bu);
        }
    }
}
