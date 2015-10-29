﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder;
using Rhino.Mocks;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.Test.BLLTest
{
    /// <summary>
    /// 为客户分配客服
    /// </summary>
    [TestFixture]
    public class TestReceptionAssign
    {
        private static List<ReceptionStatus> db = new List<ReceptionStatus>();

        private class DbMocker 
        {
            
        }

        [Test]
        public void assign_customer_login()
        {


            var dalMock = MockRepository.GenerateStub<DAL.DALReceptionStatus>(string.Empty);
            var dalMemberMock = MockRepository.GenerateStub<DAL.DALMembership>(string.Empty);

            BLLReceptionStatus bll = new BLLReceptionStatus(dalMock);
            IList<DZMembership> csList = Builder<DZMembership>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.UserName = "b")
                .TheNext(1).With(x => x.UserName = "a")
                .Build();
            dalMock.Stub(x => x.GetAll<ReceptionStatus>()).Return(Builder<ReceptionStatus>.CreateListOfSize(20)
                .TheFirst(1).With(x => x.CustomerService = csList[0])
                .TheLast(19).With(x => x.CustomerService = csList[1])
                .Build());

            DZMembership customer = Builder<DZMembership>.CreateNew().Build();
            ReceptionAssigner ass = new ReceptionAssigner(new AssignStratageRandom(),
                dalMock,dalMemberMock);
             
            int forA = 0, forB = 0;
            for (int i = 0; i < 100; i++)
            {  
            Dictionary<DZMembership,DZMembership> assigned = ass.AssignCustomerLogin(customer);
                if (assigned[customer].UserName == "a") forA++;
                if (assigned[customer].UserName == "b") forB++;
        }
            Console.Write("assign to A:" + forA + ",to B:" + forB);
            decimal result = (decimal)forA / (decimal)forB;
            Assert.IsTrue(0.69m < result && 1.44m > result);
           
        }
        public void assign_when_cs_login()
        { 
            //客服登录.增加一条上线信息, 等待分配.
            //new BLLReceptionAssign().add(csname, null);
        }

        [Test]
        public void GetOnlineSessionUserTest()
        {
            var stratageMock = MockRepository.GenerateStub<IAssignStratage>();

            ReceptionAssigner assigner = new ReceptionAssigner(stratageMock);
         IList<OnlineUserSession> sessions=   assigner.GetOnlineSessionUser();
            foreach (OnlineUserSession s in sessions)
            {
                Console.WriteLine(s.username);
            }
        }
    }
}
