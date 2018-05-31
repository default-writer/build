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
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(serviceDataRepository.Repository);
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
            var container = new Container();
            container.RegisterType<ServiceDataRepository>();
            bool noException = false;
            try
            {
                container.RegisterType<ServiceDataRepository>();
                noException = true;
            }
            catch
            {
            }
            Assert.True(noException);
        }
    }
}