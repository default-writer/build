using Xunit;

namespace Build.Tests.TestSet10
{
    public class UnitTest
    {
        IContainer container;

        public UnitTest()
        {
            container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
        }
        [Fact]
        public void TestSet10_Method1()
        {
            //TestSet10
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }
        [Fact]
        public void TestSet10_Method2()
        {
            //TestSet10
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }
        [Fact]
        public void TestSet10_Method3()
        {
            //TestSet10
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }
        [Fact]
        public void TestSet10_Method4()
        {
            //TestSet10
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }
        [Fact]
        public void TestSet10_Method5()
        {
            //TestSet10
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }
        [Fact]
        public void TestSet10_Method6()
        {
            //TestSet10
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }
    }
}