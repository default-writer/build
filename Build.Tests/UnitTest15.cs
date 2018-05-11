using Xunit;

namespace Build.Tests.TestSet15
{
    public class UnitTest
    {
        private IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void TestSet15_Method1()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet15.SqlDataRepository");
            Assert.NotNull(sql);
        }

        [Fact]
        public void TestSet15_Method10()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.ServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)");
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(2018, sqlRepository.PersonId);
        }

        [Fact]
        public void TestSet15_Method11()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet15.SqlDataRepository(System.Int32)", 2018);
            Assert.Equal(2018, sql.PersonId);
        }

        [Fact]
        public void TestSet15_Method12()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.ServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)");
            Assert.Equal(2018, ((SqlDataRepository)sql.Repository).PersonId);
        }

        [Fact]
        public void TestSet15_Method13()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.WebServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)");
            Assert.Equal(2019, ((SqlDataRepository)sql.Repository).PersonId);
        }

        [Fact]
        public void TestSet15_Method2()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet15.SqlDataRepository(System.Int32)", 2018);
            Assert.NotNull(sql);
        }

        [Fact]
        public void TestSet15_Method3()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.ServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public void TestSet15_Method4()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.ServiceDataRepository(Build.Tests.TestSet15.IPersonRepository)");
            Assert.NotNull(srv);
        }

        [Fact]
        public void TestSet15_Method5()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var p = (IPersonRepository)null;
            //type information is missing so it will match System.Object. Since there is no ServiceDataRepository(System.Object) constructor, exception will be thrown
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>(p));
        }

        [Fact]
        public void TestSet15_Method6()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>();
            var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
            Assert.NotNull(srv.Repository);
        }

        [Fact]
        public void TestSet15_Method7()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(new object[] { 0 });
            var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.NotNull(sqlRepository);
        }

        [Fact]
        public void TestSet15_Method8()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(new object[] { (int)Database.WebService });
            var srv = container.CreateInstance<ServiceDataRepository>(new object[] { sql });
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(1, sqlRepository.PersonId);
        }

        [Fact]
        public void TestSet15_Method9()
        {
            //TestSet15
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>(new object[] { Database.SQL }));
        }
    }
}