using Xunit;

namespace Build.Tests.TestSet22
{
    using Classes;
    using TestSet;

    public static class UnitTest22
    {
        [Fact]
        public static void Test1()
        {
            //TestSet22
            var container = new Container(new TypeBuilderOptions()
            {
                Constructor = new InterfaceTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver()
            });
            Assert.NotNull(container);
        }

        [Fact]
        public static void Test2()
        {
            //TestSet22
            var container = new Container(new TypeBuilderOptions
            {
                Constructor = new InterfaceThisTypeConstructor(),
                Filter = new InterfaceTypeFilter(),
                Parser = new InterfaceTypeParser(),
                Resolver = new InterfaceTypeResolver()
            });
            container.RegisterType<IInterfaceSet1>();
            var c = container.CreateInstance<C>();
            Assert.NotNull(c);
        }
    }
}