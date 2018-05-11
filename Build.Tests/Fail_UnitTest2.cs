using Xunit;

namespace Build.Tests.Fail_TestSet2
{
    public class UnitTest
    {
        private IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void Fail_TestSet2_Method1()
        {
            //Fail_TestSet2
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet2_Method2()
        {
            //Fail_TestSet2
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }

        [Fact]
        public void Fail_TestSet2_Method3()
        {
            //Fail_TestSet2
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }
    }
}