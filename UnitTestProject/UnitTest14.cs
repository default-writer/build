using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet14
    {
        [TestClass]
        public class UnitTest14
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
            }

            [TestMethod]
            public void TestSet14_Method1()
            {
                //TestSet14
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet14_Method2()
            {
                //TestSet14
                var srv2 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", new object[] { null });
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet14_Method3()
            {
                //TestSet14
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet14_Method4()
            {
                //TestSet14
                var srv2 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", new object[] { null });
                Assert.IsNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet14_Method5()
            {
                //TestSet14
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                var srv2 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", new object[] { null });
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet14_Method6()
            {
                //TestSet14
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                var srv2 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", new object[] { null });
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
            [TestMethod]
            public void TestSet14_Method7()
            {
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository", new object[] { });
                Assert.AreNotEqual(srv1, null);
            }
            [TestMethod]
            public void TestSet14_Method8()
            {
                Assert.ThrowsException<TypeInstantiationException>(() => (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(System.Int32)", new object[] { }));
            }
            [TestMethod]
            public void TestSet14_Method9()
            {
                //TestSet14
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
                Assert.AreEqual(srv1.Repository, sql);
            }
        }
    }
}
