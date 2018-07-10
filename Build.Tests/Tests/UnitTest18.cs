using Build.Tests.TestSet18;
using System;
using Xunit;

namespace Build.Tests.UnitTests18
{
    public static class UnitTests
    {
        [Fact]
        public static void TestSet18_Method1()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<LazyFactory<Empty>>(func);
            var factory = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method10()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<Factory2<Empty>>(func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method11()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method12()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method13()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<Empty>>(obj, func));
        }

        [Fact]
        public static void TestSet18_Method14()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<Factory2<Empty>>(func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]()", (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method15()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method16()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<Factory2<Empty>>();
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]()", (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method17()
        {
            //TestSet18
            var container = new Container();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<Empty>>(obj));
        }

        [Fact]
        public static void TestSet18_Method18()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Null(factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method19()
        {
            //TestSet18
            var container = new Container();
            System.Func<object> func = () => null;
            container.RegisterType<Factory2<Empty>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method2()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<LazyFactory<Empty>>(func);
            var factory = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method20()
        {
            //TestSet18
            var container = new Container();
            System.Func<object> func = () => null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<Empty>), new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method21()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory3<Empty>), (object)(func));
            var factory = (Factory3<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method22()
        {
            //TestSet18
            var container = new Container();
            object obj = new object();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<Empty>), new object[] { obj }));
        }

        [Fact]
        public static void TestSet18_Method23()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory3<Empty>), new object[] { func });
            var factory = (Factory3<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method24_1()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance(typeof(Factory2<Empty>).ToString(), (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method25()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance(typeof(Factory2<Empty>) + "(" + typeof(Func<Empty>) + ")", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method26()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), (object)func);
            var factory = (Factory2<Empty>)container.CreateInstance(typeof(Factory2<Empty>) + "(" + typeof(Func<Empty>) + ")", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method27()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), new object[] { func });
            var factory = (Factory2<Empty>)container.CreateInstance(typeof(Factory2<Empty>) + "(" + typeof(Func<Empty>) + ")", (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method28()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory3<Empty>), new object[] { func });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Empty](System.Object)", (object[])null));
        }

        [Fact]
        public static void TestSet18_Method29()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), new object[] { func });
            var factory = (Factory2<Empty>)container.CreateInstance(typeof(Factory2<Empty>) + "(" + typeof(Func<Empty>) + ")", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method3()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType(typeof(LazyFactory<Empty>), new object[] { func });
            var factory = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method30()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<LazyFactory<Empty>>(func);
            var factory = (IFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method31()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<LazyFactory<Empty>>(func);
            var factory = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method32()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<LazyFactory<Empty>>(func);
            var factory1 = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            var factory2 = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(factory1.Func, factory2.Func);
        }

        [Fact]
        public static void TestSet18_Method33()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<LazyFactory<Empty>>(func);
            var count = RuntimeTypeExtensions.FindRintimeTypes(((TypeBuilder)container.Builder).Types.Values, "Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])").Length;
            Assert.Equal(1, count);
        }

        [Fact]
        public static void TestSet18_Method34()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<Empty>>(obj));
        }

        [Fact]
        public static void TestSet18_Method35()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory3<Empty>), new object[] { func });
            var factory = (Factory3<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method36()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<Empty> func = () => new Empty();
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method37()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<Factory3<Empty>>(func);
            var factory = (Factory3<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method38()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory3<Empty>), func);
            var factory = (Factory3<Empty>)container.CreateInstance(typeof(Factory3<Empty>).ToString(), (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method39()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(LazyFactory<Empty>), func);
            var factory = (LazyFactory<Empty>)container.CreateInstance(typeof(LazyFactory<Empty>).ToString(), (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method4()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType(typeof(LazyFactory<Empty>), func);
            var factory = (LazyFactory<Empty>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method40()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(LazyFactory<Empty>), func);
            var factory = (LazyFactory<Empty>)container.CreateInstance(typeof(LazyFactory<Empty>).ToString(), (object[])null);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method41()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(LazyFactory<Empty>), func);
            var factory = (LazyFactory<Empty>)container.CreateInstance(typeof(LazyFactory<Empty>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method42()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = container.CreateInstanceFromParameters<Factory2<Empty>>(typeof(Func<Empty>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method5()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<Factory2<Empty>>(func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]()", (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method6()
        {
            //TestSet18
            var container = new Container();
            var type = new Empty();
            Func<Empty> func = () => type;
            container.RegisterType<Factory2<Empty>>(func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method7()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType(typeof(Factory2<Empty>), func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty]", (object[])null);
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method8()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => null;
            container.RegisterType(typeof(Factory2<Empty>), new object[] { func });
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method9()
        {
            //TestSet18
            var container = new Container();
            Func<Empty> func = () => new Empty();
            container.RegisterType<Factory2<Empty>>(func);
            var factory = (Factory2<Empty>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Empty](System.Func`1[Build.Tests.TestSet18.Empty])", (object[])null);
            Assert.Equal(func, factory.Func);
        }
    }
}