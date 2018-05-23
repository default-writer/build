using Xunit;

namespace Build.Tests.TestSet2
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

        /// <summary>
        /// Tests the set2 method1.
        /// </summary>
        [Fact]
        public void TestSet2_Method1()
        {
            //TestSet2
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public void TestSet2_Method2()
        {
            //TestSet2
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public void TestSet2_Method3()
        {
            //TestSet2
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public void TestSet2_Method4()
        {
            //TestSet2
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public void TestSet2_Method5()
        {
            //TestSet2
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1, srv2);
        }

        [Fact]
        public void TestSet2_Method6()
        {
            //TestSet2
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public void TestSet2_Method7()
        {
            Assert.Contains("Ho ho ho()", container.RuntimeTypeAliases);
        }

        [Fact]
        public void TestSet2_Method8()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Ho ho ho()"));
        }
    }
}