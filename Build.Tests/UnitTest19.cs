using Build.Tests.TestSet19;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Build.Tests.UnitTests19
{
    public static class UnitTests
    {
        [Fact]
        public static void TestSet18_Method1()
        {
            //TestSet19
            var container = new Container();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<Class1>());
        }

        [Fact]
        public static void TestSet18_Method2()
        {
            //TestSet19
            var container = new Container();
            container.RegisterType<Class1>();
            var class1 = container.CreateInstance<Class1>();
            Assert.NotNull(class1);
        }

        [Fact]
        public static void TestSet18_Method3()
        {
            //TestSet19
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var class1 = container.CreateInstance<Class2>();
            Assert.NotNull(class1);
        }

        [Fact]
        public static void TestSet18_Method5()
        {
            //TestSet19
            var container = new Container();
            container.RegisterType<Class2>((Func<Class1>)(() => new Class1()));
            var class1 = container.CreateInstance<Class2>();
            Assert.NotNull(class1);
        }

        [Fact]
        public static void TestSet18_Method6()
        {
            //TestSet19
            var container = new Container();
            container.RegisterType<Class2>((Func<Class1>)(() => new Class1()));
            var class1 = container.GetInstance<Class2>();
            Assert.NotNull(class1);
        }
    }
}