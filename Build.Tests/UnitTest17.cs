using Build.Tests.TestSet7;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Build.Tests.TestSet17
{
    public static class UnitTest
    {
        [Fact]
        public static void TestSet7_Method7()
        {
            //TestSet7
            var container = new Container();
            container.RegisterType<Type>();

            var runtimeType = new RuntimeType(new InjectionAttribute("Build.Tests.TestSet17.Type"), typeof(Type), true);
            runtimeType.SetRuntimeInstance(RuntimeInstance.CreateInstance);

            Assert.Throws<TypeInstantiationException>(() => runtimeType.CreateInstance("Build.Tests.TestSet17.Type"));
        }
    }
}