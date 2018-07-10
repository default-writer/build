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
            container.RegisterType<PrivateSqlDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", (object[])null));
        }

        [Fact]
        public static void TestSet1_Method21()
        {
            //TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            container.RegisterType<PrivateSqlDataRepository>();
            var srv1 = container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", (object[])null);
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
            Assert.NotNull(container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository", (object[])null));
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