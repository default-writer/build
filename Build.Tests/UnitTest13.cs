using Xunit;

namespace Build.Tests.TestSet13
{
    public class UnitTest
    {
        readonly Container container;
        readonly ServiceDataRepository srv1;
        readonly ServiceDataRepository srv2;

        public UnitTest()
        {
            container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            srv1 = container.CreateInstance<ServiceDataRepository>();
            srv2 = container.CreateInstance<ServiceDataRepository>();
        }

        [Fact]
        public void TestSet13_Method1()
        {
            //TestSet13
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet13_Method2()
        {
            //TestSet13
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet13_Method3()
        {
            //TestSet13
            TestSet13_Method1();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet13_Method4()
        {
            //TestSet13
            TestSet13_Method2();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet13_Method5()
        {
            //TestSet13
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public void TestSet13_Method6()
        {
            //TestSet13
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }
    }
}
