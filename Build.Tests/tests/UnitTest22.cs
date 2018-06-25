using Xunit;

namespace Build.Tests.TestSet22
{
    public static class UnitTest22
    {
        [Fact]
        public static void Test1()
        {
            var container = new Container(new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            Assert.NotNull(container);
        }

        [Fact]
        public static void Test2()
        {
            //TestSet16
            var container = new Container(new InterfaceThisTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
            container.RegisterType<IInterfaceSet1>();
            var c2 = container.CreateInstance<C2>();
            Assert.NotNull(c2);
        }
    }
}