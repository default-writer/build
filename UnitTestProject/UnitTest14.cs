using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace TestSet14
    {
        [TestClass]
        public class UnitTest14
        {
            Container commonPersonContainer;
            SqlDataRepository sql;
            ServiceDataRepository srv1, srv2;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                sql = new SqlDataRepository();
                srv1 = (ServiceDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                srv2 = (ServiceDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", new object[] { null });
            }

            [TestMethod]
            public void TestSet14_Method1()
            {
                //TestSet14
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet14_Method2()
            {
                //TestSet14
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet14_Method3()
            {
                //TestSet14
                TestSet14_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet14_Method4()
            {
                //TestSet14
                TestSet14_Method2();
                Assert.IsNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet14_Method5()
            {
                //TestSet14
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet14_Method6()
            {
                //TestSet14
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
            [TestMethod]
            public void TestSet14_Method7()
            {
                Assert.ThrowsException<Exception>(() => (ServiceDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet14.ServiceDataRepository", new object[] { }));
            }
            [TestMethod]
            public void TestSet14_Method8()
            {
                Assert.ThrowsException<Exception>(() => (ServiceDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(System.Int32)", new object[] { }));
            }
        }
    }
}
