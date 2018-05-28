using System;
using System.Reflection;
using Xunit;

namespace Build.Tests.TestSet0
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void TestSet0_Method1()
        {
            //TestSet0
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            bool exception = false;
            try
            {
                container.Reset();
            }
            catch (Exception)
            {
                exception = true;
            }
            Assert.False(exception);
        }

        [Fact]
        public void TestSet0_Method2()
        {
            //TestSet0
            var constructors = typeof(DefaultSqlDataRepository).GetConstructors();
            var constructorParameters = constructors[0].GetParameters();
            var injectionAttribute = (constructorParameters[0]).GetCustomAttribute<InjectionAttribute>();
            Assert.Equal(RuntimeInstance.None, injectionAttribute.RuntimeInstance);
        }

        [Fact]
        public void TestSet0_Method3()
        {
            //TestSet0
            var constructors = typeof(DefaultSqlDataRepository).GetConstructors();
            var dependencyAttribute = (constructors[0]).GetCustomAttribute<DependencyAttribute>();
            Assert.Equal(RuntimeInstance.CreateInstance, dependencyAttribute.RuntimeInstance);
        }

        [Fact]
        public void TestSet0_Method4()
        {
            //TestSet0
            var dependencyAttributeType = typeof(DependencyAttribute);
            var dependencyAttribute = Activator.CreateInstance(dependencyAttributeType);
            Assert.NotNull(dependencyAttribute);
        }
    }
}
