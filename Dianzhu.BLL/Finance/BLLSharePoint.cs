﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Finance
{
    
    public class BLLSharePoint
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Bll.Finance.BllSharePoint");
        DAL.Finance.DALSharePoint dalSharePoint;
        DAL.Finance.DALDefaultSharePoint dalDefaultSharePoint;
        public BLLSharePoint() : this(new DAL.Finance.DALSharePoint(), new DAL.Finance.DALDefaultSharePoint()) { }
        public BLLSharePoint(DAL.Finance.DALSharePoint dalSharePoint, DAL.Finance.DALDefaultSharePoint dalDefaultSharePoint)
        {
            this.dalSharePoint = dalSharePoint;
            this.dalDefaultSharePoint = dalDefaultSharePoint;
        }
        public decimal GetSharePoint(Model.DZMembership member)
        {
            decimal point = dalSharePoint.GetSharePoint(member).Point;
            decimal defaultPoint = dalDefaultSharePoint.GetDefaultSharePoint(member.UserType).Point;
            decimal finalPoint= point > 0 ? point : defaultPoint > 0 ? defaultPoint : 0;
            string errMsg = string.Empty;
            if (finalPoint == 0) {
                errMsg = "该用户及其对应的用户类型未设置分成比例" + member.DisplayName;
                log.Error(errMsg);
                throw new Exception(errMsg);
            }
            return finalPoint;
        }
        
    }
}
