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
    }
}