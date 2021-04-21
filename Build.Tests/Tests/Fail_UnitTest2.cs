using System;
using Xunit;
using Build;

namespace Fail_TestSet2
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet2_Method1()
        {
            //Fail_TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>((Type[])null));
        }

        [Fact]
        public static void Fail_TestSet2_Method10()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Fail_TestSet2_Method11()
        {
            //Fail_TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(new int?(1));
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Fail_TestSet2_Method12()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<ErrorStruct>();
            container.RegisterType<ErrorSqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ErrorSqlDataRepository), typeof(ErrorStruct)));
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

        [Fact]
        public static void Fail_TestSet2_Method4()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>(default(int)));
        }

        [Fact]
        public static void Fail_TestSet2_Method5()
        {
            //Fail_TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            var sql = container.CreateInstance<SqlDataRepository>(new int?());
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Fail_TestSet2_Method6()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet2_Method7()
        {
            //Fail_TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet2_Method9()
        {
            //Fail_TestSet2
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.NotNull(container.CreateInstance<SqlDataRepository>(1));
        }
    }
}