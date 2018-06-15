using System;
using System.Diagnostics;

namespace Build.Tests
{
    static class Program
    {
        public static void Test1()
        {
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterAssembly(typeof(Build.Tests.TestSet16.SqlDataRepository).Assembly, new string[] {
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
            var sql = (Build.Tests.TestSet1.PrivateSqlDataRepository)container.CreateInstance(typeof(Build.Tests.TestSet1.PrivateSqlDataRepository).FullName, System.Array.Empty<object>());
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.IsNotNull(sql);
        }

        public static void Test2()
        {
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterType<Build.Tests.TestSet16.SqlDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.ServiceDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.WebServiceDataRepository>();
            var sql = (Build.Tests.TestSet16.ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.ServiceDataRepository(Build.Tests.TestSet16.SqlDataRepository)");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.AreEqual(2018, ((Build.Tests.TestSet16.SqlDataRepository)sql.Repository).RepositoryId);
        }

        public static void Test3()
        {
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterType<Build.Tests.TestSet16.SqlDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.ServiceDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.WebServiceDataRepository>();
            var sql = (Build.Tests.TestSet16.WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.ServiceDataRepository)");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.AreEqual(2019, ((Build.Tests.TestSet16.ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        public static void Test4()
        {
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterType<Build.Tests.TestSet16.SqlDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.ServiceDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.WebServiceDataRepository>();
            var sql = (Build.Tests.TestSet16.WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.IPersonRepository,Build.Tests.TestSet16.IPersonRepository)");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.AreEqual(2020, ((Build.Tests.TestSet16.ServiceDataRepository)sql.RepositoryA).RepositoryId);
        }

        public static void Test5()
        {
            //TestSet16
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterType<Build.Tests.TestSet16.SqlDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.ServiceDataRepository>();
            container.RegisterType<Build.Tests.TestSet16.WebServiceDataRepository>();
            var sql = (Build.Tests.TestSet16.WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.IPersonRepository,Build.Tests.TestSet16.IPersonRepository)");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.AreEqual(2021, ((Build.Tests.TestSet16.SqlDataRepository)sql.RepositoryB).RepositoryId);
        }

        public static void Test6()
        {
            //TestSet16
            var sw = Stopwatch.StartNew();
            var container = new Container();
            container.RegisterType<Build.Tests.TestSet16.WebServiceDataRepository>();
            var sql = (Build.Tests.TestSet16.WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.IPersonRepository,Build.Tests.TestSet16.IPersonRepository)");
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            //Assert.AreEqual(2021, ((Build.Tests.TestSet16.SqlDataRepository)sql.RepositoryB).RepositoryId);
        }

        static void Main()
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
        }
    }
}