using Xunit;
using Build;

namespace TestSet2
{
    public static class UnitTest
    {
        /// <summary>
        /// Tests the set2 method1.
        /// </summary>
        [Fact]
        public static void Method1()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method2()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1, srv2);
        }

        [Fact]
        public static void Method6()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public static void Method7()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Contains("Ho ho ho()", container.RuntimeTypeAliases);
        }

        [Fact]
        public static void Method8()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Ho ho ho()"));
        }
    }
}