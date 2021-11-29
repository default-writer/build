using Xunit;
using Build;

namespace TestSet1
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method10()
        {
            //TestSet1
            var container = new Container();
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference2",
                "TestSet1.CircularReference1"
            });
            var sql = (PrivateSqlDataRepository)container.CreateInstance(typeof(PrivateSqlDataRepository).ToString(), System.Array.Empty<object>());
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Method11()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, null));
        }

        [Fact]
        public static void Method12()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference1"
            });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(PrivateSqlDataRepository2).ToString(), System.Array.Empty<object>()));
        }

        [Fact]
        public static void Method13()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository"
            });
            Assert.Throws<TypeInstantiationException>(() => (CircularReference2)container.CreateInstance("#1()"));
        }

        [Fact]
        public static void Method14()
        {
            //TestSet1
            var typeConstructor = new TypeConstructor();
            var typeFilter = new TypeFilter();
            var typeParser = new TypeParser();
            var typeResolver = new TypeResolver();
            var container = new Container(new TypeActivator(), typeConstructor, typeFilter, typeParser, typeResolver);
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method15()
        {
            //TestSet1
            var typeConstructor = new TypeConstructor();
            var typeFilter = new TypeFilter();
            var typeParser = new TypeParser();
            var typeResolver = new TypeResolver();
            var typeActivator = new TypeActivator();
            var container = new Container(typeActivator, typeConstructor, typeFilter, typeParser, typeResolver);
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method16()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference2",
                "TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void Method17()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false, UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void Method18()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false, UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository"
            }));
        }

        [Fact]
        public static void Method19()
        {
            //TestSet1
            var container = new Container();
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference2",
                "TestSet1.CircularReference1"
            });
            var sql = (PrivateSqlDataRepository)container.CreateInstance(typeof(PrivateSqlDataRepository).ToString());
            Assert.Equal(0, sql.PersonId);
        }

        [Fact]
        public static void Method2()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void Method20()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method21()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("TestSet1.PrivateSqlDataRepository", 0);
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method22()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            Assert.NotNull(container.CreateInstance("TestSet1.PrivateSqlDataRepository"));
        }

        [Fact]
        public static void Method23()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            Assert.NotNull(container.CreateInstance("TestSet1.PrivateSqlDataRepository"));
        }

        [Fact]
        public static void Method24()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Fail_TestSet7.SqlDataRepository",
                "Fail_TestSet6.ServiceDataRepository",
                "Fail_TestSet6.SqlDataRepository",
                "Fail_TestSet5.ServiceDataRepository",
                "Fail_TestSet5.SqlDataRepository",
                "Fail_TestSet4.SqlDataRepository(.*)",
                "Fail_TestSet4.ServiceDataRepository(.*)",
                "Fail_TestSet3.SqlDataRepository",
                "Fail_TestSet2.ServiceDataRepository",
                "Fail_TestSet1.Other",
                "Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Fail_TestSet1.ServiceDataRepository",
                "TestSet1.PrivateSqlDataRepository2",
                "TestSet1.CircularReference3",
                "TestSet1.CircularReference2",
                "TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void Method25()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository");
            Assert.Equal(0, sql.PersonId);
        }

        [Fact]
        public static void Method26()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("TestSet1.PrivateSqlDataRepository", 2);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void Method27()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository", 2);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method28()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 3);
            Assert.Equal(3, sql.PersonId);
        }

        [Fact]
        public static void Method29()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void Method30()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var sql = (PrivateSqlDataRepository)container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method31()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var type = container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.True(type != null && type.GetType() == typeof(PrivateSqlDataRepository));
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method32()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var type = container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.True(type != null && type.GetType() == typeof(PrivateSqlDataRepository));
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method33()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var type = container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository", 1);
            Assert.True(type != null && type.GetType() == typeof(PrivateSqlDataRepository));
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void Method34()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var type = container.CreateInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("TestSet1.PrivateSqlDataRepository", 1);
            Assert.True(type != null && type.GetType() == typeof(PrivateSqlDataRepository));
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method35()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void Method36()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method37()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("TestSet1.PrivateSqlDataRepository()", 1));
        }

        [Fact]
        public static void Method38()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository(System.Int32)", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void Method39()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public static void Method6()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotEqual(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public static void Method7()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("TestSet1.PrivateSqlDataRepository");
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method8()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            string name = null;
            Assert.Throws<TypeInstantiationException>(() => (PrivateSqlDataRepository)container.CreateInstance(name));
        }

        [Fact]
        public static void Method9()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            string name = null;
            Assert.Throws<TypeInstantiationException>(() => (PrivateSqlDataRepository)container.CreateInstance(name, System.Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet1_Method0()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method1()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method2()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = true });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method3()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = true, UseDefaultTypeResolution = true });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method4()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = true, UseDefaultTypeResolution = false });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method5()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false, UseDefaultTypeResolution = true });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method6()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false, UseDefaultTypeResolution = false });
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Instance });
            Assert.True(instance.GetPersonId() == 2021);
        }

        [Fact]
        public static void TestSet1_Method7()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository), new ParameterSource[] { ParameterSource.Default });
            Assert.True(instance.GetPersonId() == 1);
        }

        [Fact]
        public static void TestSet1_Method8()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<PersonRepository>(1);
            var instance = (PersonRepository)container.CreateInstance(typeof(PersonRepository));
            Assert.True(instance.GetPersonId() == 1);
        }
    }
}