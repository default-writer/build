using Xunit;

namespace Build.Tests.Fail_TestSet2
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet2_Method1()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet2_Method2()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }

        [Fact]
        public static void Fail_TestSet2_Method3()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }
    }
}