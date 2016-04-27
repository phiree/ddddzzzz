using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.ApplicationService.BusinessUser
{
  public   class BusinessUserService:IBusinessUserService
    {
        public BusinessUserDto Create(BusinessUserDto businessUserDto)
        {
            //先调用identityaccess 创建一个用户,获取创建的Id,用于该商户的创建
            string identityId = string.Empty;
            DomainModel.BusinessUser businessUser = new DomainModel.BusinessUser(
               identityId,  businessUserDto.RealName
                );
            return businessUserDto;
        }

         
    }
}
