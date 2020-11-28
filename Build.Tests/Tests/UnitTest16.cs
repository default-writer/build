using Xunit;
using Build;

namespace TestSet16
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("TestSet16.SqlDataRepository");
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Method10()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv = (ServiceDataRepository)container.CreateInstance("TestSet16.ServiceDataRepository(TestSet16.IPersonRepository)");
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(2018, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Method11()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("TestSet16.SqlDataRepository(System.Int32)", new object[] { 2018 });
            Assert.Equal(2018, sql.RepositoryId);
        }

        [Fact]
        public static void Method12()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (ServiceDataRepository)container.CreateInstance("TestSet16.ServiceDataRepository(TestSet16.SqlDataRepository)");
            Assert.Equal(2018, ((SqlDataRepository)sql.Repository).RepositoryId);
        }

        [Fact]
        public static void Method13()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (WebServiceDataRepository)container.CreateInstance("TestSet16.WebServiceDataRepository(TestSet16.ServiceDataRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Method14()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (WebServiceDataRepository)container.CreateInstance("TestSet16.WebServiceDataRepository(TestSet16.IPersonRepository,TestSet16.IPersonRepository)");
            Assert.Equal(2020, ((ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Method15()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (WebServiceDataRepository)container.CreateInstance("TestSet16.WebServiceDataRepository(TestSet16.IPersonRepository,TestSet16.IPersonRepository)");
            Assert.Equal(2021, ((SqlDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Method16()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            var sql = (WebServiceDataRepository)container.CreateInstance("TestSet16.WebServiceDataRepository(TestSet16.ServiceDataRepository)");
            Assert.Equal(0, sql.RepositoryId);
        }

        [Fact]
        public static void Method17()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.IPersonRepository,TestSet16.IPersonRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Method18()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.IPersonRepository,TestSet16.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Method19()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.IPersonRepository,TestSet16.SqlDataRepository)"));
        }

        [Fact]
        public static void Method2()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            var sql = (SqlDataRepository)container.CreateInstance("TestSet16.SqlDataRepository(System.Int32)", new object[] { 2018 });
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Method20()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.ServiceDataRepository,TestSet16.ServiceDataRepository)"));
        }

        [Fact]
        public static void Method21()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.SqlDataRepository,TestSet16.SqlDataRepository)"));
        }

        [Fact]
        public static void Method22()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.ServiceDataRepository,TestSet16.SqlDataRepository)"));
        }

        [Fact]
        public static void Method23()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.SqlDataRepository,TestSet16.ServiceDataRepository)");
            Assert.Equal(2020, ((SqlDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Method24()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository2>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("TestSet16.WebServiceDataRepository2(TestSet16.SqlDataRepository,TestSet16.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Method25()
        {
            //TestSet16
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = (ServiceDataRepository)container.CreateInstance("TestSet16.ServiceDataRepository(TestSet16.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Method26()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<WebServiceDataRepository>();
            container.Lock();
            var sql = (WebServiceDataRepository)container.CreateInstance("TestSet16.WebServiceDataRepository(TestSet16.IPersonRepository,TestSet16.IPersonRepository)");
            Assert.Equal(2021, ((SqlDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = (ServiceDataRepository)container.CreateInstance("TestSet16.ServiceDataRepository(TestSet16.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv = (ServiceDataRepository)container.CreateInstance("TestSet16.ServiceDataRepository(TestSet16.IPersonRepository)");
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv = container.CreateInstance<ServiceDataRepository>(0);
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Method6()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>();
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            Assert.NotNull(srv.Repository);
        }

        [Fact]
        public static void Method7()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(0);
            var srv = container.CreateInstance<ServiceDataRepository>(sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.NotNull(sqlRepository);
        }

        [Fact]
        public static void Method8()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>((int)Database.WebService);
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(1, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Method9()
        {
            //TestSet16
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>(Database.SQL));
        }
    }
}