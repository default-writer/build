using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet15
    {
        [TestClass]
        public class UnitTest15
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void TestSet15_Method1()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                var sql = (SqlDataRepository)container.CreateInstance("UnitTests.TestSet15.SqlDataRepository");
                Assert.AreNotEqual(sql, null);
            }
            [TestMethod]
            public void TestSet15_Method2()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                var sql = (SqlDataRepository)container.CreateInstance("UnitTests.TestSet15.SqlDataRepository(System.Int32)", 2018);
                Assert.AreNotEqual(sql, null);
            }
            [TestMethod]
            public void TestSet15_Method3()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var sql = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.SqlDataRepository)");
                Assert.AreNotEqual(sql.Repository, null);
            }
            [TestMethod]
            public void TestSet15_Method4()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var srv = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.IPersonRepository)");
                Assert.AreNotEqual(srv, null);
            }
            [TestMethod]
            public void TestSet15_Method5()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var srv = container.CreateInstance<ServiceDataRepository>(new object[] { null });
                Assert.AreNotEqual(srv, null);
            }
            [TestMethod]
            public void TestSet15_Method6()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var sql = container.CreateInstance<SqlDataRepository>();
                var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
                Assert.AreNotEqual(srv.Repository, null);
            }
            [TestMethod]
            public void TestSet15_Method7()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var sql = container.CreateInstance<SqlDataRepository>(new object[] { 0 });
                var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
                var sqlRepository = srv.Repository as SqlDataRepository;
                Assert.AreNotEqual(sqlRepository, null);
            }
            [TestMethod]
            public void TestSet15_Method8()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var sql = container.CreateInstance<SqlDataRepository>(new object[] { (int)Database.WebService });
                var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
                var sqlRepository = srv.Repository as SqlDataRepository;
                Assert.AreEqual(sqlRepository.PersonId, 1);
            }
            [TestMethod]
            public void TestSet15_Method9()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>(new object[] { Database.SQL }));
            }
            [TestMethod]
            public void TestSet15_Method10()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var srv = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.SqlDataRepository)");
                var sqlRepository = srv.Repository as SqlDataRepository;
                Assert.AreEqual(2018, sqlRepository.PersonId);
            }
            [TestMethod]
            public void TestSet15_Method11()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                var sql = (SqlDataRepository)container.CreateInstance("UnitTests.TestSet15.SqlDataRepository(System.Int32)", 2018);
                Assert.AreEqual(2018, sql.PersonId);
            }
            [TestMethod]
            public void TestSet15_Method12()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                var sql = (ServiceDataRepository)container.CreateInstance("UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.SqlDataRepository)");
                Assert.AreEqual(2018, ((SqlDataRepository)sql.Repository).PersonId);
            }
            [TestMethod]
            public void TestSet15_Method13()
            {
                //TestSet15
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
                container.RegisterType<WebServiceDataRepository>();
                var sql = (WebServiceDataRepository)container.CreateInstance("UnitTests.TestSet15.WebServiceDataRepository(UnitTests.TestSet15.SqlDataRepository)");
                Assert.AreEqual(2019, ((SqlDataRepository)sql.Repository).PersonId);
            }
        }
    }
}
