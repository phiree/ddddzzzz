using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAccess.DomainModel
{

    public enum enum_LoginType
    {
        /// <summary>
        /// 原生登录用户
        /// </summary>
        original = 0,

        //第三方登录用户
        WeChat = 1,
        SinaWeiBo = 2,
        TencentQQ = 3,

    }
}
