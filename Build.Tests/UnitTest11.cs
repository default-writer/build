using Xunit;

namespace Build.Tests.TestSet11
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void TestSet11_Method1()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet11_Method10()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method11()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet11_Method12()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method13()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet11_Method14()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet11_Method15()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet11_Method16()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method17()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet11_Method18()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method19()
        {
            //TestSet11
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet11_Method2()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet11_Method20()
        {
            //TestSet11
            container.RegisterType(typeof(ServiceDataRepository));
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet11_Method21()
        {
            //TestSet11
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet11_Method22()
        {
            //TestSet11
            container.RegisterType(typeof(ServiceDataRepository));
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method23()
        {
            //TestSet11
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method24()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType(typeof(ServiceDataRepository));
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet11_Method3()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet11_Method4()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method5()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet11_Method6()
        {
            //TestSet11
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet11_Method7()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet11_Method8()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet11_Method9()
        {
            //TestSet11
            container.RegisterType(typeof(SqlDataRepository));
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }
    }
}
