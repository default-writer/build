using Xunit;

namespace Build.Tests.Fail_TestSet3
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void Fail_TestSet3_Method1()
        {
            //Fail_TestSet3
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet3_Method2()
        {
            //Fail_TestSet3
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet3_Method3()
        {
            //Fail_TestSet3
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }

        [Fact]
        public void Fail_TestSet3_Method4()
        {
            //Fail_TestSet3
            container.RegisterType<ServiceDataRepository>();
            Assert.NotNull(container.CreateInstance<ServiceDataRepository>());
        }
    }
}