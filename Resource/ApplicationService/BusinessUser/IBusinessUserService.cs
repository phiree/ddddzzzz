﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.ApplicationService.BusinessUser
{
   public interface IBusinessUserService
    {
        BusinessUserDto Create(BusinessUserDto businessUserDto);
    }
}
