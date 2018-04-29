using Xunit;

namespace Build.Tests.TestSet3
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
        public void TestSet3_Method1()
        {
            //TestSet3
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
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
    }
}