using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet15
    {
        [TestClass]
        public class UnitTest15
        {
            SqlDataRepository sql;
            ServiceDataRepository srv;
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void TestSet15_Method1()
            {
                //TestSet15
                commonPersonContainer.RegisterType<SqlDataRepository>();
                sql = (SqlDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet15.SqlDataRepository");
                Assert.AreNotEqual(sql, null);
            }
            [TestMethod]
            public void TestSet15_Method2()
            {
                //TestSet15
                commonPersonContainer.RegisterType<SqlDataRepository>();
                sql = (SqlDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet15.SqlDataRepository(System.Int32)", 2018);
                Assert.AreNotEqual(sql, null);
            }
            [TestMethod]
            public void TestSet15_Method3()
            {
                //TestSet15
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                srv = (ServiceDataRepository)commonPersonContainer.CreateInstance("UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.SqlDataRepository)", new object[] { null });
                Assert.AreNotEqual(srv, null);
            }
        }
    }
}
