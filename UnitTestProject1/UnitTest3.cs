using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        TestSet3.ServiceDataRepository srv1, srv2;

        [TestInitialize]
        public void Initialize()
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<TestSet3.SqlDataRepository>();
            commonPersonContainer.RegisterType<TestSet3.ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            srv1 = commonPersonContainer.CreateInstance<TestSet3.ServiceDataRepository>();
            srv2 = commonPersonContainer.CreateInstance<TestSet3.ServiceDataRepository>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //TestSet3
            Assert.IsNotNull(srv1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //TestSet3
            Assert.IsNotNull(srv2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //TestSet3
            TestMethod1();
            Assert.IsNotNull(srv1.Repository);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //TestSet3
            TestMethod2();
            Assert.IsNotNull(srv2.Repository);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //TestSet3
            Assert.AreNotEqual(srv1, srv2);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //TestSet3
            Assert.AreEqual(srv1.Repository, srv2.Repository);
        }
    }
}
