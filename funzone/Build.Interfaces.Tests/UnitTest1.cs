using System;
using Xunit;

namespace Build.Interfaces.Tests
{
    public static class UnitTest1
    {
        [Fact]
        public static void Test1()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            Assert.NotNull(container);
        }

        [Fact]
        public static void Test10()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository(Build.Interfaces.Tests.ServiceDataRepository)");
            Assert.Equal(0, sql.RepositoryId);
        }

        [Fact]
        public static void Test11()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.IPersonRepository,Build.Interfaces.Tests.IPersonRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test12()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.IPersonRepository,Build.Interfaces.Tests.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test13()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.IPersonRepository,Build.Interfaces.Tests.SqlDataRepository)"));
        }

        [Fact]
        public static void Test14()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(System.Int32)", 2018);
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Test15()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.ServiceDataRepository,Build.Interfaces.Tests.ServiceDataRepository)"));
        }

        [Fact]
        public static void Test16()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.SqlDataRepository,Build.Interfaces.Tests.SqlDataRepository)"));
        }

        [Fact]
        public static void Test17()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            Assert.Throws<TypeInstantiationException>(() => (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.ServiceDataRepository,Build.Interfaces.Tests.SqlDataRepository)"));
        }

        [Fact]
        public static void Test18()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.SqlDataRepository,Build.Interfaces.Tests.ServiceDataRepository)");
            Assert.Equal(2020, ((SqlDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Test19()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet4>();
            var sql = (WebServiceDataRepository2)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository2(Build.Interfaces.Tests.SqlDataRepository,Build.Interfaces.Tests.ServiceDataRepository)");
            Assert.Equal(2021, ((ServiceDataRepository)sql.RepositoryB).RepositoryId);
        }

        [Fact]
        public static void Test2()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet>();
            var type1 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            Assert.NotNull(type1);
        }

        [Fact]
        public static void Test20()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver(), false, true, true);
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.ServiceDataRepository(Build.Interfaces.Tests.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Test21()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.ServiceDataRepository(Build.Interfaces.Tests.SqlDataRepository)");
            Assert.NotNull(sql.Repository);
        }

        [Fact]
        public static void Test22()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.ServiceDataRepository(Build.Interfaces.Tests.IPersonRepository)");
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Test23()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var srv = container.CreateInstance<ServiceDataRepository>(0);
            Assert.NotNull(srv);
        }

        [Fact]
        public static void Test24()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>(0);
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            Assert.NotNull(srv.Repository);
        }

        [Fact]
        public static void Test25()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>(0);
            var srv = container.CreateInstance<ServiceDataRepository>(sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.NotNull(sqlRepository);
        }

        [Fact]
        public static void Test26()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var sql = container.CreateInstance<SqlDataRepository>((int)Database.WebService);
            var srv = container.CreateInstance<ServiceDataRepository>((IPersonRepository)sql);
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(1, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Test27()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>(Database.SQL));
        }

        [Fact]
        public static void Test28()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver(), true, true, false);
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IMyFunRuleSet2_Overwrite>());
        }

        [Fact]
        public static void Test29()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2_ValueType>();
            container.RegisterType<IMyFunRuleSet2>();
            var sql1 = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(System.Int32)", 2020);
            container.RegisterType<IMyFunRuleSet2_Overwrite>();
            var sql2 = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(Build.Interfaces.Tests.IValueType)");
            Assert.Equal(sql1.RepositoryId, sql2.RepositoryId);
        }

        [Fact]
        public static void Test3()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2_ValueType>();
            container.RegisterType<IMyFunRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(System.Int32)");
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Test30()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2_ValueType>();
            container.RegisterType<IMyFunRuleSet2>();
            var sql1 = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(Build.Interfaces.Tests.IValueType)");
            container.RegisterType<IMyFunRuleSet2_Overwrite>();
            var sql2 = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(Build.Interfaces.Tests.IValueType)");
            Assert.NotEqual(sql1.RepositoryId, sql2.RepositoryId);
        }

        [Fact]
        public static void Test31()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository(Build.Interfaces.Tests.IPersonRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryC).RepositoryId);
        }

        [Fact]
        public static void Test32()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            var sql = container.CreateInstance<SqlDataRepository>(default(int));
            Assert.Equal(0, sql.RepositoryId);
        }

        [Fact]
        public static void Test33()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2_ValueType>();
            container.RegisterType<IMyFunRuleSet2>();
            // If your type have multiple constructors available, you must specify type with parameters for the build
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository"));
        }

        [Fact]
        public static void Test34()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            // If your type have multiple constructors available, you must specify type with parameters for the build
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
        }

        [Fact]
        public static void Test35()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet>();
            var type1 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            var type2 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            Assert.Equal(type1, type2);
        }

        [Fact]
        public static void Test36()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet_CreateInstance>();
            var type1 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            var type2 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            Assert.NotEqual(type1, type2);
        }

        [Fact]
        public static void Test37()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet_Enum>();
            var type = container.CreateInstance("Build.Interfaces.Tests.Type2(Build.Interfaces.Tests.MyFun)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test38()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet_Enum2>();
            var type = container.CreateInstance("Build.Interfaces.Tests.Type3(Build.Interfaces.Tests.MyFun2)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test39()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet_Enum>();
            var type = container.CreateInstance("Build.Interfaces.Tests.Type2(Build.Interfaces.Tests.MyFun2)");
            Assert.NotNull(type);
        }

        [Fact]
        public static void Test4()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            var srv = (ServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.ServiceDataRepository(Build.Interfaces.Tests.IPersonRepository)");
            var sqlRepository = srv.Repository as SqlDataRepository;
            Assert.Equal(2018, sqlRepository.RepositoryId);
        }

        [Fact]
        public static void Test40()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet_Enum>();
            var type = (Type2)container.CreateInstance("Build.Interfaces.Tests.Type2(Build.Interfaces.Tests.MyFun2)");
            Assert.Equal(-1, (int)type.MyFun2);
        }

        [Fact]
        public static void Test5()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            var sql = (SqlDataRepository)container.CreateInstance("Build.Interfaces.Tests.SqlDataRepository(System.Int32)", 2018);
            Assert.Equal(2018, sql.RepositoryId);
        }

        [Fact]
        public static void Test6()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (ServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.ServiceDataRepository(Build.Interfaces.Tests.SqlDataRepository)");
            Assert.Equal(2018, ((SqlDataRepository)sql.Repository).RepositoryId);
        }

        [Fact]
        public static void Test7()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository(Build.Interfaces.Tests.ServiceDataRepository)");
            Assert.Equal(2019, ((ServiceDataRepository)sql.RepositoryC).RepositoryId);
        }

        [Fact]
        public static void Test8()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository(Build.Interfaces.Tests.IPersonRepository,Build.Interfaces.Tests.IPersonRepository)");
            Assert.Equal(2020, ((ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        [Fact]
        public static void Test9()
        {
            //TestSet16
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRuleSet2>();
            container.RegisterType<IMyFunRuleSet2_Overwrite>();
            container.RegisterType<IMyFunRuleSet1>();
            container.RegisterType<IMyFunRuleSet3>();
            var sql = (WebServiceDataRepository)container.CreateInstance("Build.Interfaces.Tests.WebServiceDataRepository(Build.Interfaces.Tests.IPersonRepository,Build.Interfaces.Tests.IPersonRepository)");
            Assert.Equal(2021, ((SqlDataRepository)sql.RepositoryB).RepositoryId);
        }
    }
}