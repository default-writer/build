using Xunit;

namespace Build.Tests.TestSet23
{
    using Classes;

    public static class UnitTest23
    {
        [Fact]
        public static void Test1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions()
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            Assert.NotNull(container);
        }

        [Fact]
        public static void Test10()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>(aobj, bobj);
            var c = container.CreateInstance<C>();
            Assert.Equal(bobj, c.B);
        }

        [Fact]
        public static void Test11()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>(aobj, bobj);
            var c = (C)container.CreateInstance("Build.Tests.TestSet23.C(Build.Tests.TestSet23.A,Build.Tests.TestSet23.B)");
            Assert.Equal(aobj, c.A);
        }

        [Fact]
        public static void Test12()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>(aobj, bobj);
            var c = (C)container.CreateInstance("Build.Tests.TestSet23.C(Build.Tests.TestSet23.A,Build.Tests.TestSet23.B)");
            Assert.Equal(bobj, c.B);
        }

        [Fact]
        public static void Test2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            container.RegisterType<C>();
            var c = container.CreateInstance<C>();
            Assert.NotNull(c);
        }

        [Fact]
        public static void Test3()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            container.RegisterType<C>();
            var c = container.CreateInstance<C>();
            Assert.NotNull(c.A);
        }

        [Fact]
        public static void Test4()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            container.RegisterType<C>();
            var c = container.CreateInstance<C>();
            Assert.NotNull(c.B);
        }

        [Fact]
        public static void Test5()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>();
            var c = container.CreateInstance<C>(aobj, bobj);
            Assert.NotNull(c);
        }

        [Fact]
        public static void Test6()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>();
            var c = container.CreateInstance<C>(aobj, bobj);
            Assert.Equal(aobj, c.A);
        }

        [Fact]
        public static void Test7()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>();
            var c = container.CreateInstance<C>(aobj, bobj);
            Assert.Equal(bobj, c.B);
        }

        [Fact]
        public static void Test8()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>(aobj, bobj);
            var c = container.CreateInstance<C>();
            Assert.NotNull(c);
        }

        [Fact]
        public static void Test9()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<C>(aobj, bobj);
            var c = container.CreateInstance<C>();
            Assert.Equal(aobj, c.A);
        }
    }
}