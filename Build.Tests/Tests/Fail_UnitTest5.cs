using Xunit;

namespace Build.Tests.Fail_TestSet5
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet5_Method1()
        {
            //Fail_TestSet5
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet5_Method2()
        {
            //Fail_TestSet5
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet5_Method3()
        {
            //Fail_TestSet5
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet5_Method4()
        {
            //Fail_TestSet5
            var container = new Container();
            container.RegisterType<OtherRepository>();
            container.RegisterType<NoSqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<NoSqlDataRepository>());
        }
    }
}