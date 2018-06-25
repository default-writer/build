using System.Linq;
using Xunit;

namespace Build.Tests.TestSet3
{
    public static class UnitTest
    {
        [Fact]
        public static void TestSet3_Method1()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void TestSet3_Method10()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(p => container.CreateInstance(p, default(int)));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void TestSet3_Method11()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(p => container.CreateInstance(p));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void TestSet3_Method12()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (int)container.CreateInstance("System.Int32()");
            Assert.True(instance == 0);
        }

        [Fact]
        public static void TestSet3_Method13()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (int)container.CreateInstance("System.Int32()", 123);
            Assert.True(instance == 0);
        }

        [Fact]
        public static void TestSet3_Method14()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet3.ServiceDataRepository(Build.Tests.TestSet3.IPersonRepository)");
            Assert.NotNull(instance);
        }

        [Fact]
        public static void TestSet3_Method15()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet3.ServiceDataRepository(System.Int32)"));
        }

        [Fact]
        public static void TestSet3_Method16()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)");
            Assert.NotNull(instance);
        }

        [Fact]
        public static void TestSet3_Method17()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)", 123);
            Assert.Equal(123, instance.PersonId);
        }

        [Fact]
        public static void TestSet3_Method18()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)", 123);
            var instance2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)");
            Assert.True(instance1.PersonId == 123 && instance2.PersonId == 0);
        }

        [Fact]
        public static void TestSet3_Method19()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            // Build.Tests.TestSet3.ServiceDataRepository parameters do not match
            var instances = container.RuntimeTypes.Where(p => p != "Build.Tests.TestSet3.ServiceDataRepository(Build.Tests.TestSet3.IPersonRepository)").Select(p => container.CreateInstance(p, default(int)));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void TestSet3_Method2()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void TestSet3_Method20()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(p => container.CreateInstance(p, null));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void TestSet3_Method21()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(p => container.CreateInstance(p, null));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void TestSet3_Method3()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void TestSet3_Method4()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public static void TestSet3_Method5()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public static void TestSet3_Method6()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public static void TestSet3_Method7()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Contains("Ho ho ho()", container.RuntimeTypeAliases);
        }

        [Fact]
        public static void TestSet3_Method8()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance("Ho ho ho()");
            Assert.NotNull(sql);
        }

        [Fact]
        public static void TestSet3_Method9()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypeAliases.Select(p => container.CreateInstance(p));
            Assert.True(instances.All(p => p != null));
        }
    }
}