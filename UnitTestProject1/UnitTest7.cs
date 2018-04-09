using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest7
    {
        TestSet7.ServiceDataRepository srv1, srv2;

        [TestInitialize]
        public void Initialize()
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<TestSet7.SqlDataRepository>();
            commonPersonContainer.RegisterType<TestSet7.ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            srv1 = commonPersonContainer.CreateInstance<TestSet7.ServiceDataRepository>();
            srv2 = commonPersonContainer.CreateInstance<TestSet7.ServiceDataRepository>();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //TestSet7
            Assert.IsNotNull(srv1);
        }
        [TestMethod]
        public void TestMethod2()
        {
            //TestSet7
            Assert.IsNotNull(srv2);
        }
        [TestMethod]
        public void TestMethod3()
        {
            //TestSet7
            TestMethod1();
            Assert.IsNotNull(srv1.Repository);
        }
        [TestMethod]
        public void TestMethod4()
        {
            //TestSet7
            TestMethod2();
            Assert.IsNotNull(srv2.Repository);
        }
        [TestMethod]
        public void TestMethod5()
        {
            //TestSet7
            Assert.AreNotEqual(srv1, srv2);
        }
        [TestMethod]
        public void TestMethod6()
        {
            //TestSet7
            Assert.AreNotEqual(srv1.Repository, srv2.Repository);
        }
    }
}
