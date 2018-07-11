using System;
using Xunit;

namespace Build.Tests.TestSet21
{
    using Classes;

    public static class UnitTest21
    {
        [Fact]
        public static void Test1()
        {
            //TestSet21
            var container = new Container(new TypeBuilderOptions()
            {
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver()
            });
            Assert.NotNull(container);
        }

        [Fact]
        public static void Test10()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(0, sql.RepositoryId);
        }

        [Fact]
        public static void Test11()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.IPersonRepository,Build.Tests.TestSet21.IPersonRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test12()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.IPersonRepository,Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test13()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.IPersonRepository,Build.Tests.TestSet21.SqlDataRepository)"));
        }

        [Fact]
        public static void Test14()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(System.Int32)", 2018);
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Test15()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.ServiceDataRepository,Build.Tests.TestSet21.ServiceDataRepository)"));
        }

        [Fact]
        public static void Test16()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.SqlDataRepository,Build.Tests.TestSet21.SqlDataRepository)"));
        }

        [Fact]
        public static void Test17()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.ServiceDataRepository,Build.Tests.TestSet21.SqlDataRepository)"));
        }

        [Fact]
        public static void Test18()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.SqlDataRepository,Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(2020, ((SqlDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Test19()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository2(Build.Tests.TestSet21.SqlDataRepository,Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test2()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet>();
            var type1 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
            Assert.NotNull(type1);
        }

        [Fact]
        public static void Test20()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.ServiceDataRepository(Build.Tests.TestSet21.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Test21()
        {
            //TestSet21
            var typeBuilderOptions = new TypeBuilderOptions()
            {
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver(),
                UseValueTypes = false
            };
            var container = new Container(typeBuilderOptions);
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.ServiceDataRepository(Build.Tests.TestSet21.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Test22()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.ServiceDataRepository(Build.Tests.TestSet21.IPersonRepository)");
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Test23()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var srv = container.CreateInstance<ServiceDataRepository>(0);
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Test24()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>(0);
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            Assert.NotNull(srv.Repository);
        }

        [Fact]
        public static void Test25()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>(0);
            var srv = container.CreateInstance<ServiceDataRepository>(sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.NotNull(sqlRepository);
        }

        [Fact]
        public static void Test26()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>((int)Database.WebService);
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(1, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Test27()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>(Database.SQL));
        }

        [Fact]
        public static void Test28()
        {
            //TestSet21
            var typeBuilderOptions = new TypeBuilderOptions()
            {
                Activator = new TypeActivator(),
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver(),
                UseDefaultTypeAttributeOverwrite = false,
                UseValueTypes = false
            };
            var container = new Container(typeBuilderOptions);
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IInterfaceRuleSet2_Overwrite>());
        }

        [Fact]
        public static void Test29()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            var sql1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(System.Int32)", 2020);
            container.RegisterType<IInterfaceRuleSet2_Overwrite>();
            var sql2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(Build.Tests.TestSet21.IValueType)");
            Assert.Equal(sql1.RepositoryId, sql2.RepositoryId);
        }

        [Fact]
        public static void Test3()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(System.Int32)");
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Test30()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            var sql1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(Build.Tests.TestSet21.IValueType)");
            container.RegisterType<IInterfaceRuleSet2_Overwrite>();
            var sql2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(Build.Tests.TestSet21.IValueType)");
            Assert.NotEqual(sql1.RepositoryId, sql2.RepositoryId);
        }

        [Fact]
        public static void Test31()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.IPersonRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryC).RepositoryId);
        }

        [Fact]
        public static void Test32()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            var sql = container.CreateInstance<SqlDataRepository>(default(int));
            Assert.Equal(0, sql.RepositoryId);
        }

        [Fact]
        public static void Test33_1()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            // If your type have multiple constructors available, you must specify type with parameters for the build
            // unless you are using a default constructor
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository"));
        }

        [Fact]
        public static void Test33_2()
        {
            //TestSet21
            var container = new Container(new TypeBuilderOptions()
            {
                Activator = new TypeActivator(),
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver(),
                UseDefaultConstructor = true,
                UseValueTypes = false
            });
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            // If your type have multiple constructors available, you must specify type with parameters for the build
            // unless you are using a default constructor
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository"));
        }

        [Fact]
        public static void Test34()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            // If your type have multiple constructors available, you must specify type with parameters for the build
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Test35()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet>();
            var type1 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
            var type2 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
            Assert.Equal(type1, type2);
        }

        [Fact]
        public static void Test36()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet_CreateInstance>();
            var type1 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
            var type2 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
            Assert.NotEqual(type1, type2);
        }

        [Fact]
        public static void Test37()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet_Enum>();
            var type = container.CreateInstance("Build.Tests.TestSet21.Type2(Build.Tests.TestSet21.Interface)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test38()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet_Enum2>();
            var type = container.CreateInstance("Build.Tests.TestSet21.Type3(Build.Tests.TestSet21.Interface2)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test39()
        {
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet_Enum>();
            var type = container.CreateInstance("Build.Tests.TestSet21.Type2(Build.Tests.TestSet21.Interface2)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test4()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.ServiceDataRepository(Build.Tests.TestSet21.IPersonRepository)");
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(2018, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Test40()
        {
            var container = new Container(new TypeBuilderOptions
            {
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver(),
                UseValueTypes = false
            });
            container.RegisterType<IInterfaceRuleSet_Enum>();
            var type = (Type2)container.CreateInstance("Build.Tests.TestSet21.Type2(Build.Tests.TestSet21.Interface2)");
            Assert.Equal(-1, (int)type.Interface2);
        }

        [Fact]
        public static void Test41()
        {
            //TestSet21
            var container = new Container(new TypeBuilderOptions
            {
                Constructor = new InterfaceThisTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver(),
                UseValueTypes = false
            });
            container.RegisterType<IInterfaceThisRuleSet1>();
            container.RegisterType<IInterfaceThisRuleSet2>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryC).RepositoryId);
        }

        [Fact]
        public static void Test42()
        {
            //TestSet21
            TypeBuilderOptions typeBuilderOptions = null;
            Assert.Throws<ArgumentNullException>(() => new Container(typeBuilderOptions));
        }

        [Fact]
        public static void Test43()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2_ValueType>();
            container.RegisterType<IInterfaceRuleSet2>();
            var sql1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(Build.Tests.TestSet21.ValueType)");
            container.RegisterType<IInterfaceRuleSet2_Overwrite>();
            var sql2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(Build.Tests.TestSet21.ValueType)");
            Assert.NotEqual(sql1.RepositoryId, sql2.RepositoryId);
        }

        [Fact]
        public static void Test5()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet21.SqlDataRepository(System.Int32)", 2018);
            Assert.Equal(2018, sql.RepositoryId);
        }

        [Fact]
        public static void Test6()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.ServiceDataRepository(Build.Tests.TestSet21.SqlDataRepository)");
            Assert.Equal(2018, ((SqlDataRepository)sql.Repository).RepositoryId);
        }

        [Fact]
        public static void Test7()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.ServiceDataRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryC).RepositoryId);
        }

        [Fact]
        public static void Test8()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.IPersonRepository,Build.Tests.TestSet21.IPersonRepository)");
            Assert.Equal(2020, ((ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Test9()
        {
            //TestSet21
            var container = new Container(new TypeActivator(), new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceRuleSet2>();
            container.RegisterType<IInterfaceRuleSet2_Overwrite>();
            container.RegisterType<IInterfaceRuleSet1>();
            container.RegisterType<IInterfaceRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet21.WebServiceDataRepository(Build.Tests.TestSet21.IPersonRepository,Build.Tests.TestSet21.IPersonRepository)");
            Assert.Equal(2021, ((SqlDataRepository)sql.RepositoryB).RepositoryId);
        }
    }
}