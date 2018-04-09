using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        TestSet2.ServiceDataRepository srv1, srv2;

        [TestInitialize]
        public void Initialize()
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<TestSet2.SqlDataRepository>();
            commonPersonContainer.RegisterType<TestSet2.ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            srv1 = commonPersonContainer.CreateInstance<TestSet2.ServiceDataRepository>();
            srv2 = commonPersonContainer.CreateInstance<TestSet2.ServiceDataRepository>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //TestSet2
            Assert.IsNotNull(srv1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //TestSet2
            Assert.IsNotNull(srv2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //TestSet2
            TestMethod1();
            Assert.IsNotNull(srv1.Repository);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //TestSet2
            TestMethod2();
            Assert.IsNotNull(srv2.Repository);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //TestSet2
            Assert.AreEqual(srv1, srv2);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //TestSet2
            Assert.AreEqual(srv1.Repository, srv2.Repository);
        }
    }
}
