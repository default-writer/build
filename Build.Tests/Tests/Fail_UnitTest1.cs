using System;
using Xunit;

namespace Build.Tests.Fail_TestSet1
{
    public static class UnitTest
    {
        [Fact]
        public static void Fail_TestSet1_Method1()
        {
            //Fail_TestSet1
            var container = new Container();
            try
            {
                container.RegisterType<SqlDataRepository>();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public static void Fail_TestSet1_Method10()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method11()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method12()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterType<ServiceDataRepository2>();
            var sql = container.CreateInstance<ServiceDataRepository2>();
            Assert.Null(sql.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method13()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method14()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>("Build.Tests.Fail_TestSet1.Other2"));
        }

        [Fact]
        public static void Fail_TestSet1_Method15()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository3>());
        }

        [Fact]
        public static void Fail_TestSet1_Method16()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository4>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository4>());
        }

        [Fact]
        public static void Fail_TestSet1_Method17()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository5>());
        }

        [Fact]
        public static void Fail_TestSet1_Method18()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method19()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(IPersonRepository), Array.Empty<object>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method2()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<PrivateConstructorServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method20()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository5), Array.Empty<object>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method21()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository5), (Type[])null));
        }

        [Fact]
        public static void Fail_TestSet1_Method22()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions());
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method23()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions());
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method24()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<MarshalByRefObject>());
        }

        [Fact]
        public static void Fail_TestSet1_Method25()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Attribute>());
        }

        [Fact]
        public static void Fail_TestSet1_Method26()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<InjectionAttribute>());
        }

        [Fact]
        public static void Fail_TestSet1_Method27()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<DependencyAttribute>());
        }

        [Fact]
        public static void Fail_TestSet1_Method28()
        {
            //Fail_TestSet1
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Type>());
        }

        [Fact]
        public static void Fail_TestSet1_Method29()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method3()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>());
        }

        [Fact]
        public static void Fail_TestSet1_Method30()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository4>();
            var srv1 = container.CreateInstance<ServiceDataRepository4>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method31()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = container.CreateInstance<ServiceDataRepository5>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method32()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = (ServiceDataRepository5)container.CreateInstance(typeof(ServiceDataRepository5), Array.Empty<object>());
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method33()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = (ServiceDataRepository5)container.CreateInstance(typeof(ServiceDataRepository5), Array.Empty<object>());
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method34()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method36()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>(Array.Empty<object>());
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method37()
        {
            //Fail_TestSet1
            // Automatic type instantiation disabled
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>());
        }

        [Fact]
        public static void Fail_TestSet1_Method38()
        {
            //Fail_TestSet1
            // Automatic type instantiation disabled
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false, UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>());
        }

        [Fact]
        public static void Fail_TestSet1_Method39()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            var sql = container.CreateInstance<ServiceDataRepository2>();
            Assert.Null(sql.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method4()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IPersonRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method40()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository), typeof(IPersonRepository)));
        }

        [Fact]
        public static void Fail_TestSet1_Method41()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository).ToString(), typeof(IPersonRepository).ToString()));
        }

        [Fact]
        public static void Fail_TestSet1_Method42()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(IPersonRepository).ToString(), Array.Empty<string>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method43()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository5>((Type[])null));
        }

        [Fact]
        public static void Fail_TestSet1_Method44()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository5>(Array.Empty<Type>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method45()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            var sql = container.CreateInstance<ServiceDataRepository5>(typeof(IPersonRepository));
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Fail_TestSet1_Method46()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            var sql = container.CreateInstance<ServiceDataRepository5>(typeof(IPersonRepository).ToString());
            Assert.NotNull(sql);
        }

        [Fact]
        public static void Fail_TestSet1_Method47()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository5)));
        }

        [Fact]
        public static void Fail_TestSet1_Method48()
        {
            //Fail_TestSet1
            var container = new Container();
            IdType instance = null;
            container.RegisterType<ErrorClass>(instance);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance<ErrorClass>());
        }

        [Fact]
        public static void Fail_TestSet1_Method49()
        {
            //Fail_TestSet1
            var container = new Container();
            IdType instance = null;
            container.RegisterType<ErrorClass>(instance);
            var error = container.CreateInstance<ErrorClass>();
            Assert.NotNull(error);
        }

        [Fact]
        public static void Fail_TestSet1_Method5()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = true });
            container.RegisterType<ServiceDataRepository2>();
            Assert.NotNull(container.CreateInstance<ServiceDataRepository2>());
        }

        [Fact]
        public static void Fail_TestSet1_Method50()
        {
            //Fail_TestSet1
            var container = new Container();
            IdType instance = null;
            container.RegisterType<ErrorClass>(instance);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ErrorClass>((IdType)null));
        }

        [Fact]
        public static void Fail_TestSet1_Method51()
        {
            //Fail_TestSet1
            var container = new Container();
            IdType instance = null;
            container.RegisterType<ErrorClass>(instance);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet1.ErrorClass(Build.Tests.TestSet1.IdType)", (IdType)null));
        }

        [Fact]
        public static void Fail_TestSet1_Method52()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeResolution = false });
            container.RegisterType<ErrorServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ErrorServiceDataRepository2>());
        }

        [Fact]
        public static void Fail_TestSet1_Method6()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<Other2>();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method7()
        {
            //Fail_TestSet1
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeInstantiation = false });
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>(Array.Empty<object>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method8()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>(Array.Empty<object>()));
        }

        [Fact]
        public static void Fail_TestSet1_Method9()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>("Build.Tests.Fail_TestSet1.Other"));
        }
    }
}