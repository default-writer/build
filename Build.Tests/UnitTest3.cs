using System.Linq;
using Xunit;

namespace Build.Tests.TestSet3
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
        public void TestSet3_Method1()
        {
            //TestSet3
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet3_Method10()
        {
            var instances = container.RuntimeAliasedTypes.Select(p => container.CreateInstance(p));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public void TestSet3_Method11()
        {
            var instances = container.RuntimeNonAliasedTypes.Select(p => container.CreateInstance(p));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public void TestSet3_Method12()
        {
            var instance = (int)container.CreateInstance("System.Int32()");
            Assert.True(instance == 0);
        }

        [Fact]
        public void TestSet3_Method13()
        {
            var instance = (int)container.CreateInstance("System.Int32()", 123);
            Assert.True(instance == 0);
        }

        [Fact]
        public void TestSet3_Method14()
        {
            var instance = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet3.ServiceDataRepository(Build.Tests.TestSet3.IPersonRepository)");
            Assert.NotNull(instance);
        }

        [Fact]
        public void TestSet3_Method15()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet3.ServiceDataRepository(System.Int32)"));
        }

        [Fact]
        public void TestSet3_Method16()
        {
            var instance = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)");
            Assert.NotNull(instance);
        }

        [Fact]
        public void TestSet3_Method17()
        {
            var instance = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)", 123);
            Assert.Equal(123, instance.PersinId);
        }

        [Fact]
        public void TestSet3_Method18()
        {
            var instance1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)", 123);
            var instance2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)");
            Assert.Equal(0, instance2.PersinId);
        }

        [Fact]
        public void TestSet3_Method2()
        {
            //TestSet3
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet3_Method3()
        {
            //TestSet3
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet3_Method4()
        {
            //TestSet3
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet3_Method5()
        {
            //TestSet3
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet3_Method6()
        {
            //TestSet3
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet3_Method7()
        {
            Assert.Contains("Ho ho ho()", container.RuntimeTypeAliases);
        }

        [Fact]
        public void TestSet3_Method8()
        {
            var sql = container.CreateInstance("Ho ho ho()");
            Assert.NotNull(sql);
        }

        [Fact]
        public void TestSet3_Method9()
        {
            var instances = container.RuntimeTypeAliases.Select(p => container.CreateInstance(p));
            Assert.True(instances.All(p => p != null));
        }
    }
}