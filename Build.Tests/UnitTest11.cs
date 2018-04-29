using Xunit;

namespace Build.Tests.TestSet11
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
        public void TestSet11_Method1()
        {
            //TestSet11
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }
        [Fact]
        public void TestSet11_Method2()
        {
            //TestSet11
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }
        [Fact]
        public void TestSet11_Method3()
        {
            //TestSet11
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }
        [Fact]
        public void TestSet11_Method4()
        {
            //TestSet11
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }
        [Fact]
        public void TestSet11_Method5()
        {
            //TestSet11
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }
        [Fact]
        public void TestSet11_Method6()
        {
            //TestSet11
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }
    }
}