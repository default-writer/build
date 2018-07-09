using System.Linq;
using Xunit;

namespace Build.Tests.Fail_TestSet7
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet7_Method1()
        {
            //Fail_TestSet7
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet7_Method2()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet7_Method3()
        {
            //Fail_TestSet7
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet7_Method4()
        {
            //Fail_TestSet7
            var container = new Container();
            container.RegisterType<OtherRepository>();
            container.RegisterType<ServiceDataRepository>();
            var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(serviceDataRepository.Repository);
        }

        [Fact]
        public static void Fail_TestSet7_Method5()
        {
            //Fail_TestSet7
            var container = new Container(new TypeBuilderOptions() { UseDefaultTypeAttributeOverwrite = false });
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet7_Method6()
        {
            //Fail_TestSet7
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(serviceDataRepository.Repository);
        }

        [Fact]
        public static void Fail_TestSet7_Method7()
        {
            //Fail_TestSet7
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.True(container.RuntimeTypes.Any());
        }
    }
}