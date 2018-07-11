using Xunit;

namespace Build.Tests.TestSet1
{
    public static class UnitTest
    {
        [Fact]
        public static void TestSet1_Method1()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void TestSet1_Method10()
        {
            //TestSet1
            var container = new Container();
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference2",
                "Build.Tests.TestSet1.CircularReference1"
            });
            var sql = (PrivateSqlDataRepository)container.CreateInstance(typeof(PrivateSqlDataRepository).ToString(), System.Array.Empty<object>());
            Assert.NotNull(sql);
        }

        [Fact]
        public static void TestSet1_Method11()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, null));
        }

        [Fact]
        public static void TestSet1_Method12()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference1"
            });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(PrivateSqlDataRepository2).ToString(), System.Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet1_Method13()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository"
            });
            Assert.Throws<TypeInstantiationException>(() => (CircularReference2)container.CreateInstance("#1()"));
        }

        [Fact]
        public static void TestSet1_Method14()
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
        public static void TestSet1_Method15()
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
        public static void TestSet1_Method16()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference2",
                "Build.Tests.TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void TestSet1_Method17()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false, UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void TestSet1_Method18()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false, UseDefaultTypeAttributeOverwrite = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository"
            }));
        }

        [Fact]
        public static void TestSet1_Method19()
        {
            //TestSet1
            var container = new Container();
            container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference2",
                "Build.Tests.TestSet1.CircularReference1"
            });
            var sql = (PrivateSqlDataRepository)container.CreateInstance(typeof(PrivateSqlDataRepository).ToString());
            Assert.Equal(0, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method2()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void TestSet1_Method20()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method21()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 0);
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void TestSet1_Method22()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            Assert.NotNull(container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository"));
        }

        [Fact]
        public static void TestSet1_Method23()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            Assert.NotNull(container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository"));
        }

        [Fact]
        public static void TestSet1_Method24()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterAssembly(typeof(PrivateSqlDataRepository).Assembly, new string[] {
                "Build.Tests.Fail_TestSet7.SqlDataRepository",
                "Build.Tests.Fail_TestSet6.ServiceDataRepository",
                "Build.Tests.Fail_TestSet6.SqlDataRepository",
                "Build.Tests.Fail_TestSet5.ServiceDataRepository",
                "Build.Tests.Fail_TestSet5.SqlDataRepository",
                "Build.Tests.Fail_TestSet4.SqlDataRepository(.*)",
                "Build.Tests.Fail_TestSet4.ServiceDataRepository(.*)",
                "Build.Tests.Fail_TestSet3.SqlDataRepository",
                "Build.Tests.Fail_TestSet2.ServiceDataRepository",
                "Build.Tests.Fail_TestSet1.Other",
                "Build.Tests.Fail_TestSet1.PrivateConstructorServiceDataRepository",
                "Build.Tests.Fail_TestSet1.ServiceDataRepository",
                "Build.Tests.TestSet1.PrivateSqlDataRepository2",
                "Build.Tests.TestSet1.CircularReference3",
                "Build.Tests.TestSet1.CircularReference2",
                "Build.Tests.TestSet1.CircularReference1"
            }));
        }

        [Fact]
        public static void TestSet1_Method25()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository");
            Assert.Equal(0, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method26()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 2);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method27()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 2);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method28()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 3);
            Assert.Equal(3, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method29()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method3()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void TestSet1_Method30()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var sql = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method31()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql2 = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method32()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(1);
            var sql2 = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)");
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method33()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql2 = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method34()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql2 = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method35()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method36()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method37()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository()", 1));
        }

        [Fact]
        public static void TestSet1_Method38()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository(System.Int32)", 1);
            Assert.Equal(1, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method39()
        {
            //TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>(2);
            var sql = (PrivateSqlDataRepository)container.GetInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", 1);
            Assert.Equal(2, sql.PersonId);
        }

        [Fact]
        public static void TestSet1_Method4()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public static void TestSet1_Method5()
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
        public static void TestSet1_Method6()
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
        public static void TestSet1_Method7()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository");
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void TestSet1_Method8()
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
        public static void TestSet1_Method9()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            string name = null;
            Assert.Throws<TypeInstantiationException>(() => (PrivateSqlDataRepository)container.CreateInstance(name, System.Array.Empty<object>()));
        }
    }
}