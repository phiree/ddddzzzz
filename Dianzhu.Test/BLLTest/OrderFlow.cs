﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
namespace Dianzhu.Test.BLLTest
{

    [TestFixture]
    public class OrderFlow
    {
        
        [Test]
        
        public void ChangeStatus()
        {
            ServiceOrder order = Builder<ServiceOrder>.CreateNew().With(x => x.OrderStatus = Model.Enums.enum_OrderStatus.Created)
                .Build();
            try
            {
                new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Begin);
            }
            catch (Exception exe)
            {
                Assert.AreEqual(exe.GetType(), typeof(Exception));
            }

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Payed);
            Assert.AreEqual(enum_OrderStatus.Payed, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Negotiate);
            Assert.AreEqual(enum_OrderStatus.Negotiate, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Assigned);
            Assert.AreEqual(enum_OrderStatus.Assigned, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Begin);
            Assert.AreEqual(enum_OrderStatus.Begin, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.IsEnd);
            Assert.AreEqual(enum_OrderStatus.IsEnd, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Ended);
            Assert.AreEqual(enum_OrderStatus.Ended, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Finished);
            Assert.AreEqual(enum_OrderStatus.Finished, order.OrderStatus);

            new OrderServiceFlow().ChangeStatus(order, Model.Enums.enum_OrderStatus.Appraise);
            Assert.AreEqual(enum_OrderStatus.Appraise, order.OrderStatus);





         

        }
    }
}