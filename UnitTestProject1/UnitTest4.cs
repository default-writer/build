using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest4
    {
        TestSet4.ServiceDataRepository srv1, srv2;

        [TestInitialize]
        public void Initialize()
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<TestSet4.SqlDataRepository>();
            commonPersonContainer.RegisterType<TestSet4.ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            srv1 = commonPersonContainer.CreateInstance<TestSet4.ServiceDataRepository>();
            srv2 = commonPersonContainer.CreateInstance<TestSet4.ServiceDataRepository>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //TestSet4
            Assert.IsNotNull(srv1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //TestSet4
            Assert.IsNotNull(srv2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //TestSet4
            TestMethod1();
            Assert.IsNotNull(srv1.Repository);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //TestSet4
            TestMethod2();
            Assert.IsNotNull(srv2.Repository);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //TestSet4
            Assert.AreNotEqual(srv1, srv2);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //TestSet4
            Assert.AreEqual(srv1.Repository, srv2.Repository);
        }
    }
}
