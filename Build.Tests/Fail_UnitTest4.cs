using Xunit;

namespace Build.Tests.Fail_TestSet4
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet4_Method1()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method2()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method3()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method4()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method5()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }
    }
}