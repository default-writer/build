using Xunit;

namespace Build.Tests.Fail_TestSet5
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void Fail_TestSet5_Method1()
        {
            //Fail_TestSet5
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet5_Method2()
        {
            //Fail_TestSet5
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public void Fail_TestSet5_Method3()
        {
            //Fail_TestSet5
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public void Fail_TestSet5_Method4()
        {
            //Fail_TestSet5
            container.RegisterType<OtherRepository>();
            container.RegisterType<NoSqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<NoSqlDataRepository>());
        }
    }
}
