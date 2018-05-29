using Xunit;

namespace Build.Tests.Fail_TestSet6
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void Fail_TestSet6_Method1()
        {
            //Fail_TestSet6
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet6_Method2()
        {
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public void Fail_TestSet6_Method3()
        {
            container.RegisterType<PublicDataRepository>();
            Assert.NotNull(container.CreateInstance<PublicDataRepository>());
        }
    }
}
