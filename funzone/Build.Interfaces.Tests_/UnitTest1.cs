using Xunit;

namespace Build.Interfaces.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            Assert.NotNull(container);
        }

        [Fact]
        public void Test2()
        {
            var container = new Container(new MyFunTypeConstructor(), new MyFunTypeFilter(), new MyFunTypeParser(), new MyFunTypeResolver());
            container.RegisterType<IMyFunRules>();
            var type1 = container.CreateInstance("Build.Interfaces.Tests.Type1(Build.Interfaces.Tests.Arg1,Build.Interfaces.Tests.Arg2)");
            Assert.NotNull(type1);
        }
    }
}