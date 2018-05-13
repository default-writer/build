using Xunit;

namespace Build.Tests.TestSet14
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
        }

        [Fact]
        public void TestSet14_Method1()
        {
            //TestSet14
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", sql);
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet14_Method2()
        {
            //TestSet14
            var srv2 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet14_Method3()
        {
            //TestSet14
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", sql);
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet14_Method4()
        {
            //TestSet14
            var srv2 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", new object[] { null });
            Assert.Null(srv2.Repository);
        }

        [Fact]
        public void TestSet14_Method5()
        {
            //TestSet14
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", sql);
            var srv2 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet14_Method6()
        {
            //TestSet14
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", sql);
            var srv2 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", new object[] { null });
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet14_Method7()
        {
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository", System.Array.Empty<object>());
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet14_Method8()
        {
            Assert.Throws<TypeInstantiationException>(() => (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(System.Int32)", System.Array.Empty<object>()));
        }

        [Fact]
        public void TestSet14_Method9()
        {
            //TestSet14
            var sql = new SqlDataRepository();
            var srv1 = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet14.ServiceDataRepository(Build.Tests.TestSet14.SqlDataRepository)", sql);
            Assert.Equal(sql, srv1.Repository);
        }
    }
}