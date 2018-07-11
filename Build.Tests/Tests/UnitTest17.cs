using Xunit;

namespace Build.Tests.TestSet17
{
    public static class UnitTest
    {
        [Fact]
        public static void TestSet17_Method1()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("Build.Tests.TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance(new object[] { "Build.Tests.TestSet17.Type" }));
        }

        [Fact]
        public static void TestSet17_Method2()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("Build.Tests.TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);
            runtimeType.AddConstructorParameter(((TypeBuilder)container.Builder).CanRegister(runtimeType.ActivatorType), new RuntimeType(typeActivator, new InjectionAttribute(typeof(Type)), typeof(Type), true));
            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance(new object[] { typeof(Type) }));
        }

        [Fact]
        public static void TestSet17_Method3()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var type = (Type)container.CreateInstance("Build.Tests.TestSet17.Type(Build.Tests.TestSet17.SubType)");
            Assert.NotNull(type.SubType);
        }

        [Fact]
        public static void TestSet17_Method4()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();
            var typeActivator = new TypeActivator();
            var runtimeType = new RuntimeType(typeActivator, new InjectionAttribute("Build.Tests.TestSet17.Type"), typeof(Type), true);
            var value = runtimeType.GetValue("");
            Assert.Null(value);
        }
    }
}