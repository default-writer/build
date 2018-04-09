using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest6
    {
        TestSet6.ServiceDataRepository srv1, srv2;

        [TestInitialize]
        public void Initialize()
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<TestSet6.SqlDataRepository>();
            commonPersonContainer.RegisterType<TestSet6.ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            srv1 = commonPersonContainer.CreateInstance<TestSet6.ServiceDataRepository>();
            srv2 = commonPersonContainer.CreateInstance<TestSet6.ServiceDataRepository>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //TestSet6
            Assert.IsNotNull(srv1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //TestSet6
            Assert.IsNotNull(srv2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //TestSet6
            TestMethod1();
            Assert.IsNotNull(srv1.Repository);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //TestSet6
            TestMethod2();
            Assert.IsNotNull(srv2.Repository);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //TestSet6
            Assert.AreNotEqual(srv1, srv2);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //TestSet6
            Assert.AreNotEqual(srv1.Repository, srv2.Repository);
        }
    }
}
