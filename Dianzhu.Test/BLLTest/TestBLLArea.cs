﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder;
using Rhino.Mocks;
using Dianzhu.BLL;
 
using Dianzhu.DAL;
namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
    public  class BLLAreaTest
    {
        [Test]
        public void GetArea()
        {
       
            var dalArea = MockRepository.GenerateStub<DALArea>(string.Empty);
            List<Model.Area> list = new List<Model.Area>();
            list.Add(new Model.Area { Name="jm"});
            dalArea.Stub(x => x.GetArea(1)).Return(
                list
                );
            
             BLLArea bllArea = new  BLLArea(dalArea);
            var r= bllArea.GetArea(1);
            Assert.AreEqual("jm", r[0].Name);
        }
    }
}
