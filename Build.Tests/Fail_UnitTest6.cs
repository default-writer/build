using Xunit;

namespace Build.Tests.Fail_TestSet6
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet6_Method1()
        {
            //Fail_TestSet6
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet6_Method2()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet6_Method3()
        {
            var container = new Container();
            container.RegisterType<PublicDataRepository>();
            Assert.NotNull(container.CreateInstance<PublicDataRepository>());
        }
    }
}