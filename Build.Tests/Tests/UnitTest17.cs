using Xunit;
using Build;

namespace TestSet17
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance(new object[] { "TestSet17.Type" }));
        }

        [Fact]
        public static void Method2()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            runtimeType.AddConstructorParameter(((TypeBuilder)container.Builder).CanRegister(runtimeType.ActivatorType), new RuntimeType(typeActivator, new InjectionAttribute(typeof(Type)), typeof(Type), true));
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance(new object[] { typeof(Type) }));
        }

        [Fact]
        public static void Method3()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var type = (Type)container.CreateInstance("TestSet17.Type(TestSet17.SubType)");
            Assert.NotNull(type.SubType);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            runtimeType.AddConstructorParameter(((TypeBuilder)container.Builder).CanRegister(runtimeType.ActivatorType), new RuntimeType(typeActivator, new InjectionAttribute(typeof(Type)), typeof(Type), true));
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance((object[])null));
        }

        [Fact]
        public static void Method5()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            runtimeType.AddConstructorParameter(((TypeBuilder)container.Builder).CanRegister(runtimeType.ActivatorType), new RuntimeType(typeActivator, new InjectionAttribute(typeof(Type)), typeof(Type), true));
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance((string[])null));
        }
    }
}