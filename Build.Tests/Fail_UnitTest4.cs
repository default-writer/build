using Xunit;

namespace Build.Tests.Fail_TestSet4
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void Fail_TestSet4_Method1()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet4_Method2()
        {
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet4_Method3()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }

        [Fact]
        public void Fail_TestSet4_Method4()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public void Fail_TestSet4_Method5()
        {
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }
    }
}