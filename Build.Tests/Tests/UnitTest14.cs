using Xunit;
using Build;

namespace TestSet14
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", sql);
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method2()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", sql);
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", new object[] { null });
            Assert.Null(srv2.Repository);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", sql);
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public static void Method6()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", sql);
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public static void Method7()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository", System.Array.Empty<object>());
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method8()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(System.Int32)", System.Array.Empty<object>()));
        }

        [Fact]
        public static void Method9()
        {
            //TestSet14
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet14.ServiceDataRepository(TestSet14.SqlDataRepository)", sql);
            Assert.Equal(sql, srv1.Repository);
        }
    }
}