using Xunit;

namespace Build.Tests.TestSet9
{
    public class UnitTest
    {
        private IContainer container;

        public UnitTest()
        {
            container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
        }

        [Fact]
        public void TestSet9_Method1()
        {
            //TestSet9
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet9_Method2()
        {
            //TestSet9
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet9_Method3()
        {
            //TestSet9
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet9_Method4()
        {
            //TestSet9
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet9_Method5()
        {
            //TestSet9
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet9_Method6()
        {
            //TestSet9
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }
    }
}