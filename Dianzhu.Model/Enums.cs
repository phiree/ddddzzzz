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
        Staff_Avatar,//职员头像
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
        Created,//已创建,待付款
        Payed,//已付款
        CancelledNeedReturn,//已取消待退款
        Assigned,//已安排工作人员
        CancelledNeedReAssign,//客户已取消 等待撤销工作人员分配.
        Finished,//已完成
        Aborded,//已中止
         
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
    /// <summary>
    /// 聊天类型
    /// </summary>
    public enum enum_ChatType
    { 
        Text,
        PushedService,// 推送的服务
        ConfirmedService,//被确认的服务
        Order,//订单.
    }



}
