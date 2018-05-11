using Xunit;

namespace Build.Tests.Fail_TestSet4
{
    public class UnitTest
    {
        IContainer container;

        public UnitTest()
        {
            container = new Container();
        }
        [Fact]
        public void Fail_TestSet4_Method1()
        {
            //Fail_TestSet4
            //container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }
        [Fact]
        public void Fail_TestSet4_Method2()
        {
            //Fail_TestSet4
            //container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }
        [Fact]
        public void Fail_TestSet4_Method3()
        {
            //Fail_TestSet4
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }
        [Fact]
        public void Fail_TestSet4_Method4()
        {
            //Fail_TestSet4
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }
    }
}