using Xunit;
using Build;

namespace TestSet17
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1_0()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(Options.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance(new object[] { "TestSet17.Type" }));
        }

        [Fact]
        public static void Method1_1()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = (TypeActivator)null;
            var injectionAttribute = new InjectionAttribute("TestSet17.Type");
            var type = typeof(Type);
            Assert.Throws<System.ArgumentNullException>(() => new RuntimeType(typeActivator, injectionAttribute, type, true));
        }

        [Fact]
        public static void Method1_2()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var injectionAttribute = (InjectionAttribute)null;
            var type = typeof(Type);
            Assert.Throws<System.ArgumentNullException>(() => new RuntimeType(typeActivator, injectionAttribute, type, true));
        }

        [Fact]
        public static void Method1_3() 
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var injectionAttribute = new InjectionAttribute("TestSet17.Type");
            var type = (System.Type)null;
            Assert.Throws<System.ArgumentNullException>(() => new RuntimeType(typeActivator, injectionAttribute, type, true));
        }

        [Fact]
        public static void Method2()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(Options.CreateInstance);
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
            runtimeType.SetRuntimeInstance(Options.CreateInstance);
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
            runtimeType.SetRuntimeInstance(Options.CreateInstance);
            runtimeType.AddConstructorParameter(((TypeBuilder)container.Builder).CanRegister(runtimeType.ActivatorType), new RuntimeType(typeActivator, new InjectionAttribute(typeof(Type)), typeof(Type), true));
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance((string[])null));
        }
    }
}