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
            bool throwsException = false;
            try
            {
                container.RegisterType<SqlDataRepository>();
            }
            catch (Exception)
            {
                throwsException = true;
            }
            Assert.False(throwsException);
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
            var container = new Container(true, true);
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
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.Fail_TestSet1.ServiceDataRepository2(Build.Tests.Fail_TestSet1.Other2)"));
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
            var container = new Container(true, false);
            container.RegisterType<ServiceDataRepository4>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository4>());
        }

        [Fact]
        public static void Fail_TestSet1_Method17()
        {
            //Fail_TestSet1
            var container = new Container(true, false);
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository5>());
        }

        [Fact]
        public static void Fail_TestSet1_Method18()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>(null));
        }

        [Fact]
        public static void Fail_TestSet1_Method19()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(IPersonRepository), null));
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
            var container = new Container(true, false);
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository5)));
        }

        [Fact]
        public static void Fail_TestSet1_Method21()
        {
            //Fail_TestSet1
            var container = new Container(true, false);
            container.RegisterType<ServiceDataRepository5>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ServiceDataRepository5), null));
        }

        [Fact]
        public static void Fail_TestSet1_Method22()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
        }

        [Fact]
        public static void Fail_TestSet1_Method23()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
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
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method3()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>());
        }

        [Fact]
        public static void Fail_TestSet1_Method30()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository4>();
            var srv1 = container.CreateInstance<ServiceDataRepository4>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method31()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = container.CreateInstance<ServiceDataRepository5>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method32()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = (ServiceDataRepository5)container.CreateInstance(typeof(ServiceDataRepository5));
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method33()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository5>();
            var srv1 = (ServiceDataRepository5)container.CreateInstance(typeof(ServiceDataRepository5), null);
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method34()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>(null);
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public static void Fail_TestSet1_Method36()
        {
            //Fail_TestSet1
            var container = new Container(true, true);
            container.RegisterType<ServiceDataRepository2>();
            var srv1 = container.CreateInstance<ServiceDataRepository2>(Array.Empty<object>());
            Assert.Null(srv1.Repository);
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
        public static void Fail_TestSet1_Method5()
        {
            //Fail_TestSet1
            var container = new Container(true, false);
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>(null));
        }

        [Fact]
        public static void Fail_TestSet1_Method6()
        {
            //Fail_TestSet1
            var container = new Container();
            container.RegisterType<Other2>();
            container.RegisterType<ServiceDataRepository2>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>(null));
        }

        [Fact]
        public static void Fail_TestSet1_Method7()
        {
            //Fail_TestSet1
            var container = new Container(true, false);
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
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.Fail_TestSet1.ServiceDataRepository2(Build.Tests.Fail_TestSet1.Other)"));
        }
    }
}