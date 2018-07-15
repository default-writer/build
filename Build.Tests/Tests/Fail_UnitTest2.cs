using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Build.Tests.Fail_TestSet2
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
        public static void Fail_TestSet2_Method13()
        {
            //Fail_TestSet2
            var binaryFmt = new BinaryFormatter();
            var ms = new MemoryStream();
            var ex = new TypeRegistrationException();
            binaryFmt.Serialize(ms, ex);
            ms.Position = 0;
            ex = (TypeRegistrationException)binaryFmt.Deserialize(ms);
            Assert.NotNull(ex);
        }

        [Fact]
        public static void Fail_TestSet2_Method14()
        {
            //Fail_TestSet2
            var binaryFmt = new BinaryFormatter();
            var ms = new MemoryStream();
            var ex = new TypeInstantiationException();
            binaryFmt.Serialize(ms, ex);
            ms.Position = 0;
            ex = (TypeInstantiationException)binaryFmt.Deserialize(ms);
            Assert.NotNull(ex);
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