using Build.Tests.TestSet18;
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
            System.Func<Type> func = () => new Type();
            container.RegisterType<Lazy<Type>>(func);
            var factory = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method10()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType<Factory2<Type>>(func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method11()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method12()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method13()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<Type>>(obj, func));
        }

        [Fact]
        public static void TestSet18_Method14()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType<Factory2<Type>>(func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method15()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method16()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<Factory2<Type>>(null);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method17()
        {
            //TestSet18
            var container = new Container();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<Type>>(obj));
        }

        [Fact]
        public static void TestSet18_Method18()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Null(factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method19()
        {
            //TestSet18
            var container = new Container();
            System.Func<object> func = () => null;
            container.RegisterType(typeof(Factory2<Type>));
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])", func));
        }

        [Fact]
        public static void TestSet18_Method2()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType<Lazy<Type>>(func);
            var factory = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method20()
        {
            //TestSet18
            var container = new Container();
            System.Func<object> func = () => null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<Type>), func));
        }

        [Fact]
        public static void TestSet18_Method21()
        {
            //TestSet18
            var container = new Container(true, true, false);
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory3<Type>), (object)(func));
            var factory = (Factory3<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method22()
        {
            //TestSet18
            var container = new Container();
            object obj = new object();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<Type>), obj));
        }

        [Fact]
        public static void TestSet18_Method23()
        {
            //TestSet18
            var container = new Container(true, true, false);
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory3<Type>), new object[] { func });
            var factory = (Factory3<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method24()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance(typeof(Factory2<Type>).ToString());
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method25()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance(typeof(Factory2<Type>) + "(" + typeof(System.Func<Type>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method26()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), (object)func);
            var factory = (Factory2<Type>)container.CreateInstance(typeof(Factory2<Type>) + "(" + typeof(System.Func<Type>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method27()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance(typeof(Factory2<Type>) + "(" + typeof(System.Func<Type>) + ")");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method28()
        {
            //TestSet18
            var container = new Container(true, true, false);
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory3<Type>), new object[] { func });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.Type](System.Object)"));
        }

        [Fact]
        public static void TestSet18_Method29()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), new object[] { func });
            var factory = (Factory2<Type>)container.CreateInstance(typeof(Factory2<Type>) + "(" + typeof(System.Func<Type>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method3()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType(typeof(Lazy<Type>), func);
            var factory = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method30()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType<Lazy<Type>>(func);
            var factory = (IFactory<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method31()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType<Lazy<Type>>(func);
            var factory = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method32()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType<Lazy<Type>>(func);
            var factory1 = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            var factory2 = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(factory1.Func, factory2.Func);
        }

        [Fact]
        public static void TestSet18_Method4()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType(typeof(Lazy<Type>), func);
            var factory = (Lazy<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method5()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType<Factory2<Type>>(func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method6()
        {
            //TestSet18
            var container = new Container();
            var type = new Type();
            System.Func<Type> func = () => type;
            container.RegisterType<Factory2<Type>>(func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type]");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method7()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType(typeof(Factory2<Type>), func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type]");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method8()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => null;
            container.RegisterType(typeof(Factory2<Type>), new object[] { func });
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method9()
        {
            //TestSet18
            var container = new Container();
            System.Func<Type> func = () => new Type();
            container.RegisterType<Factory2<Type>>(func);
            var factory = (Factory2<Type>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.Type](System.Func`1[Build.Tests.TestSet18.Type])");
            Assert.Equal(func, factory.Func);
        }
    }
}