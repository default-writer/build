using System;
using System.Reflection;
using Xunit;
using Build;
using TypeFilter = Build.TypeFilter;
using Container = Build.Container;
namespace TestSet0
{
    public static class UnitTest
    {
        [Fact]
        public static void Method0_1()
        {
            //TestSet0
            var container = new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), new TypeParser(), new TypeResolver());
            Assert.NotNull(container.Builder.Constructor);
        }

        [Fact]
        public static void Method0_2()
        {
            //TestSet0
            var container = new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), new TypeParser(), new TypeResolver());
            Assert.NotNull(container.Builder.Filter);
        }

        [Fact]
        public static void Method0_3()
        {
            //TestSet0
            var container = new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), new TypeParser(), new TypeResolver());
            Assert.NotNull(container.Builder.Parser);
        }

        [Fact]
        public static void Method0_4()
        {
            //TestSet0
            var container = new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), new TypeParser(), new TypeResolver());
            Assert.NotNull(container.Builder.Resolver);
        }

        [Fact]
        public static void Method0_5()
        {
            //TestSet0
            Assert.Throws<ArgumentNullException>(() => new Container(new TypeActivator(), null, new TypeFilter(), new TypeParser(), new TypeResolver()));
        }

        [Fact]
        public static void Method0_6()
        {
            //TestSet0
            Assert.Throws<ArgumentNullException>(() => new Container(new TypeActivator(), new TypeConstructor(), null, new TypeParser(), new TypeResolver()));
        }

        [Fact]
        public static void Method0_7()
        {
            //TestSet0
            Assert.Throws<ArgumentNullException>(() => new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), null, new TypeResolver()));
        }

        [Fact]
        public static void Method0_8()
        {
            //TestSet0
            Assert.Throws<ArgumentNullException>(() => new Container(new TypeActivator(), new TypeConstructor(), new TypeFilter(), new TypeParser(), null));
        }

        [Fact]
        public static void Method1()
        {
            //TestSet0
            var container = new Container();
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            try
            {
                container.Reset();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public static void Method2()
        {
            //TestSet0
            var container = new Container();
            var constructors = typeof(DefaultSqlDataRepository).GetConstructors();
            var constructorParameters = constructors[0].GetParameters();
            var injectionAttribute = (constructorParameters[0]).GetCustomAttribute<InjectionAttribute>();
            Assert.Equal(RuntimeInstance.Exclude, injectionAttribute.RuntimeInstance);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet0
            var container = new Container();
            var constructors = typeof(DefaultSqlDataRepository).GetConstructors();
            var dependencyAttribute = (constructors[0]).GetCustomAttribute<DependencyAttribute>();
            Assert.Equal(RuntimeInstance.CreateInstance, dependencyAttribute.RuntimeInstance);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet0
            var container = new Container();
            var dependencyAttributeType = typeof(DependencyAttribute);
            var dependencyAttribute = Activator.CreateInstance(dependencyAttributeType);
            Assert.NotNull(dependencyAttribute);
        }
    }
}