using Xunit;

namespace Build.Tests.Fail_TestSet4
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet4_Method1()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method2()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method3()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method4()
        {
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method5()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet4_Method6()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository2>());
        }

        [Fact]
        public static void Fail_TestSet4_Method7()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository3>());
        }

        [Fact]
        public static void Fail_TestSet4_Method8()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository4>());
        }

        [Fact]
        public static void Fail_TestSet4_Method9()
        {
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository5>());
        }

        [Fact]
        public static void TestSet4_Method10()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository6>());
        }

        [Fact]
        public static void TestSet4_Method11()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository7>());
        }

        [Fact]
        public static void TestSet4_Method6()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository2>());
        }

        [Fact]
        public static void TestSet4_Method7()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository3>());
        }

        [Fact]
        public static void TestSet4_Method8()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository4>());
        }

        [Fact]
        public static void TestSet4_Method9()
        {
            //TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository5>());
        }
    }
}