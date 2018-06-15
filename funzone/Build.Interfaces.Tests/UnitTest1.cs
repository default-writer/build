using System;
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
    }
}