using Xunit;

namespace Build.Tests.Fail_TestSet7
{
    public class UnitTest
    {
        IContainer container;

        public UnitTest()
        {
            container = new Container();
        }
        [Fact]
        public void Fail_TestSet7_Method1()
        {
            //Fail_TestSet7
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }
        [Fact]
        public void Fail_TestSet7_Method2()
        {
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }
        [Fact]
        public void Fail_TestSet7_Method3()
        {
            //Fail_TestSet7
            container.RegisterType<ServiceDataRepository>();
            var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(serviceDataRepository.Repository);
        }
        [Fact]
        public void Fail_TestSet7_Method4()
        {
            //Fail_TestSet7
            container.RegisterType<OtherRepository>();
            container.RegisterType<ServiceDataRepository>();
            var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(serviceDataRepository.Repository);
        }
        [Fact]
        public void Fail_TestSet7_Method5()
        {
            //Fail_TestSet7
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