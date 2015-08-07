﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Enums
{
    public enum enum_ImageType
    {
        Business_License,//商家营业执照
        Business_License_B,//营业执照
        Business_Show,//商家展示图片
        Business_ChargePersonIdCard,//负责人证件照片
        Business_Avatar,//店铺头像
    }
    public enum PayType
    {
        Offline=1,
        Online=2,
        None=4,
           }
    /// <summary>
    /// 计费单位
    /// </summary>
    public enum enum_ChargeUnit
    {
        Hour,//每小时
        Day, //每天
        Times //每次
    }

    public enum enum_ServiceMode
    {
        ToHouse,//上门
        NotToHouse,//不上门
    }
    public enum enum_IDCardType
    {
        身份证 = 0,
        其他 = 1,
        
        
    }

    public enum enum_OrderStatus
    {
        Wt,//Wait 
        Ry,//Ready 
        An,//Actionţ 
        Py,//Pay 
        Ee,//Evaluate 
        Nu,//Can
        Dy,//Delay 
        Ed,//End
    }
    public enum enum_OrderSearchType
    { 
    Nt,//未完成的服务
        De,//完成的服务
        ALL//全部
    }
    public enum enum_CashTicketSearchType
    { 
        /// <summary>
        /// 尚未使用的
        /// </summary>
    Nt,
        /// <summary>
        /// 已经使用的
        /// </summary>
        Us,
        /// <summary>
        /// 已经过期的
        /// </summary>
        Ps,
        /// <summary>
        ///所有
        /// </summary>
        All
    }



}
