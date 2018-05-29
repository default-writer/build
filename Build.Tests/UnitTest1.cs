using Xunit;

namespace Build.Tests.TestSet1
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
        public void TestSet1_Method1()
        {
            //TestSet1
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet1_Method2()
        {
            //TestSet1
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet1_Method21()
        {
            //TestSet1
            container.RegisterType<PrivateSqlDataRepository>();
            string name = null;
            Assert.Throws<TypeInstantiationException>(() => (PrivateSqlDataRepository)container.CreateInstance(name));
        }

        [Fact]
        public void TestSet1_Method22()
        {
            //TestSet1
            container.RegisterType<PrivateSqlDataRepository>();
            string name = null;
            Assert.Throws<TypeInstantiationException>(() => (PrivateSqlDataRepository)container.CreateInstance(name, System.Array.Empty<object>()));
        }

        [Fact]
        public void TestSet1_Method23()
        {
            //TestSet1
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository"
            });
            var sql = (PrivateSqlDataRepository)container.CreateInstance(typeof(PrivateSqlDataRepository).FullName, System.Array.Empty<object>());
            Assert.NotNull(sql);
        }

        [Fact]
        public void TestSet1_Method24()
        {
            //TestSet1
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, null));
        }

        [Fact]
        public void TestSet1_Method3()
        {
            //TestSet1
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet1_Method4()
        {
            //TestSet1
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet1_Method5()
        {
            //TestSet1
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet1_Method6()
        {
            //TestSet1
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet1_Method7()
        {
            //TestSet1
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository");
            Assert.NotNull(srv1);
        }
    }
}
