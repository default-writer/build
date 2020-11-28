using System.Linq;
using Xunit;
using Build;

namespace TestSet3
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method10()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(p => container.CreateInstance(p, default(int)));
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method11()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method12()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (int)container.CreateInstance("System.Int32()");
            Assert.True(instance == 0);
        }

        [Fact]
        public static void Method13()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (int)container.CreateInstance("System.Int32()", 123);
            Assert.True(instance == 0);
        }

        [Fact]
        public static void Method14()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.NotNull(instance);
        }

        [Fact]
        public static void Method15()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("TestSet3.ServiceDataRepository(System.Int32)"));
        }

        [Fact]
        public static void Method16()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (SqlDataRepository)container.CreateInstance("TestSet3.SqlDataRepository(System.Int32)");
            Assert.NotNull(instance);
        }

        [Fact]
        public static void Method17()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance = (SqlDataRepository)container.CreateInstance("TestSet3.SqlDataRepository(System.Int32)", 123);
            Assert.Equal(123, instance.PersonId);
        }

        [Fact]
        public static void Method18()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instance1 = (SqlDataRepository)container.CreateInstance("TestSet3.SqlDataRepository(System.Int32)", 123);
            var instance2 = (SqlDataRepository)container.CreateInstance("TestSet3.SqlDataRepository(System.Int32)");
            Assert.True(instance1.PersonId == 123 && instance2.PersonId == 0);
        }

        [Fact]
        public static void Method19()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            // TestSet3.ServiceDataRepository parameters match
            var instances = container.RuntimeTypes.Where(p => p == "TestSet3.ServiceDataRepository(System.Int32)").Select(p => container.CreateInstance(p, default(int)));
            Assert.True(instances.Count() == 0);
        }

        [Fact]
        public static void Method2()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void Method20()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(container.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method21()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(container.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method22()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>(0);
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method23()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>(0);
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(container.GetInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method24()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method25()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeAliasedTypes.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method26()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>(0);
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(container.GetInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method27()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(container.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method28()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(p => container.CreateInstance(p, new object[] { null }));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method29()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(p => container.CreateInstance(p, 0));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method3()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>();
            Assert.NotNull(srv1.Repository);
        }

        [Fact]
        public static void Method30()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(p => container.CreateInstance(p, new object[] { 0 }));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method31()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            // TestSet3.ServiceDataRepository parameters do not match
            var instances = container.RuntimeTypes.Where(p => p != "TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)").Select(p => container.CreateInstance(p, default(int)));
            Assert.Throws<TypeInstantiationException>(() => instances.All(p => p != null));
        }

        [Fact]
        public static void Method32()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>(0));
        }

        [Fact]
        public static void Method33()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository2>();
            var srv2 = container.CreateInstance<ServiceDataRepository2>();
            Assert.NotNull(srv2.Repository);
        }

        [Fact]
        public static void Method34()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>(0));
        }

        [Fact]
        public static void Method35()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>(0));
        }

        [Fact]
        public static void Method36()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.True(srv1.Repository != null && srv2.Repository == srv1.Repository);
        }

        [Fact]
        public static void Method37()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.True(srv2.Repository != null && srv1.Repository == srv2.Repository);
        }

        [Fact]
        public static void Method38()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.True(srv1.Repository != null && srv2.Repository == srv1.Repository);
        }

        [Fact]
        public static void Method39()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.True(srv2.Repository != null && srv1.Repository == srv2.Repository);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(srv2.Repository);
        }

        [Fact]
        public static void Method40()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)", (IPersonRepository)null);
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.False(srv2.Repository != null && srv1.Repository == srv2.Repository);
        }

        [Fact]
        public static void Method41()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)", (IPersonRepository)null);
            Assert.False(srv2.Repository != null && srv1.Repository == srv2.Repository);
        }

        [Fact]
        public static void Method42()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)", (IPersonRepository)null);
            Assert.True(srv1.Repository != null && srv2.Repository != srv1.Repository);
        }

        [Fact]
        public static void Method43()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)", (IPersonRepository)null);
            var srv2 = (ServiceDataRepository)container.CreateInstance("TestSet3.ServiceDataRepository(TestSet3.IPersonRepository)");
            Assert.True(srv2.Repository != null && srv1.Repository != srv2.Repository);
        }

        [Fact]
        public static void Method44()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypes.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method45()
        {
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Method46()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(srv2.Repository);
        }

        [Fact]
        public static void Method47()
        {
            //TestSet3
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<SqlDataRepository>(0);
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeNonAliasedTypes.Select(container.GetInstance);
            Assert.True(instances.All(p => p != null));
        }

        [Fact]
        public static void Method5()
        {
            //TestSet3
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
            //TestSet3
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            var srv2 = container.CreateInstance<ServiceDataRepository>();
            Assert.Equal(srv1.Repository, srv2.Repository);
        }

        [Fact]
        public static void Method7()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            Assert.Contains("Ho ho ho()", container.RuntimeTypeAliases);
        }

        [Fact]
        public static void Method8()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var sql = container.CreateInstance("Ho ho ho()");
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Method9()
        {
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            var instances = container.RuntimeTypeAliases.Select(container.CreateInstance);
            Assert.True(instances.All(p => p != null));
        }
    }
}