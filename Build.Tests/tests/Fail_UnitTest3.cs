using Xunit;

namespace Build.Tests.Fail_TestSet3
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet3_Method1()
        {
            //Fail_TestSet3
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet3_Method2()
        {
            //Fail_TestSet3
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet3_Method3()
        {
            //Fail_TestSet3
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }

        [Fact]
        public static void Fail_TestSet3_Method4()
        {
            //Fail_TestSet3
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet3_Method5()
        {
            //Fail_TestSet3
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            Assert.NotNull(container.CreateInstance<ServiceDataRepository>());
        }
    }
}