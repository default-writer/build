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
        public static void Test13()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor(),
                UseValueTypes = false
            });
            var d = new D();
            container.RegisterType<E>(d);
            var e = container.CreateInstance<E>();
            Assert.Equal(d, e.D);
        }

        [Fact]
        public static void Test14()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor(),
                UseValueTypes = false
            });
            var f = new F() { C = new C() };
            container.RegisterType<G>(f);
            var g = container.CreateInstance<G>();
            Assert.Equal(f.C, g.F.C);
        }

        [Fact]
        public static void Test15()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor(),
                UseValueTypes = false
            });
            var f = new F() { C = new C() };
            container.RegisterType<G>();
            var g = container.CreateInstance<G>(f);
            Assert.Equal(f, g.F);
        }

        [Fact]
        public static void Test16()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor(),
                UseValueTypes = false
            });
            var f = new F() { C = new C() };
            container.RegisterType<G>();
            var g = container.CreateInstance<G>(f);
            Assert.Equal(f.C, g.F.C);
        }

        [Fact]
        public static void Test17()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor(),
                UseValueTypes = false
            });
            var f = new F() { C = new C() { A = new A(), B = new B() } };
            container.RegisterType<C>(f.C.A, f.C.B);
            container.RegisterType<G>();
            var g = container.CreateInstance<G>(f);
            Assert.Equal(f.C.A, g.F.C.A);
        }

        [Fact]
        public static void Test18()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var h = new H() { C = new C() { A = new A(), B = new B() } };
            container.RegisterType<C>(h.C.A, h.C.B);
            container.RegisterType<H>();
            var h0 = container.CreateInstance<H>();
            Assert.NotEqual(h.C, h0.C);
        }

        [Fact]
        public static void Test19()
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
            container.RegisterType<H>();
            var h = container.GetInstance<H>();
            Assert.NotNull(h.C);
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
        public static void Test20()
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
            container.RegisterType<H>();
            var h = container.GetInstance<H>();
            Assert.NotNull(h.C.B);
        }

        [Fact]
        public static void Test21_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            var h = container.GetInstance<H>();
            Assert.Equal(aobj, h.C.A);
        }

        [Fact]
        public static void Test21_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            var h = container.GetInstance<H>();
            Assert.Equal(bobj, h.C.B);
        }

        [Fact]
        public static void Test22_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>(aobj, bobj);
            container.RegisterType<H>(cobj);
            var c = container.GetInstance<C>();
            Assert.Equal(aobj, c.A);
        }

        [Fact]
        public static void Test22_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>(aobj, bobj);
            container.RegisterType<H>(cobj);
            var c = container.GetInstance<C>();
            Assert.Equal(bobj, c.B);
        }

        [Fact]
        public static void Test23()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>(aobj, bobj);
            container.RegisterType<H>(cobj);
            var h = container.GetInstance<H>();
            Assert.Equal(cobj, h.C);
        }

        [Fact]
        public static void Test24_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            var h = container.CreateInstance<H>();
            Assert.Equal(aobj, h.C.A);
        }

        [Fact]
        public static void Test24_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            var h = container.CreateInstance<H>();
            Assert.Equal(bobj, h.C.B);
        }

        [Fact]
        public static void Test25_1()
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
            container.RegisterType<H>();
            var h = container.GetInstance<H>();
            Assert.Equal(aobj, h.C.A);
        }

        [Fact]
        public static void Test25_2()
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
            container.RegisterType<H>();
            var h = container.GetInstance<H>();
            Assert.Equal(bobj, h.C.B);
        }

        [Fact]
        public static void Test26()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>();
            container.RegisterType<H>(cobj);
            var h = container.CreateInstance<H>();
            Assert.Equal(cobj, h.C);
        }

        [Fact]
        public static void Test27()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>();
            container.RegisterType<H>(cobj);
            var h1 = container.CreateInstance<H>();
            var h2 = container.CreateInstance<H>();
            Assert.NotEqual(h1, h2);
        }

        [Fact]
        public static void Test28()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<C>();
            container.RegisterType<H>(cobj);
            var h1 = container.CreateInstance<H>();
            var h2 = container.CreateInstance<H>();
            Assert.Equal(h1.C, h2.C);
        }

        [Fact]
        public static void Test29_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            container.RegisterType<C>(aobj, bobj);
            var h2 = container.CreateInstance<H>();
            Assert.Equal(aobj, h2.C.A);
        }

        [Fact]
        public static void Test29_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            container.RegisterType<C>(aobj, bobj);
            var h2 = container.CreateInstance<H>();
            Assert.Equal(bobj, h2.C.B);
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
        public static void Test30_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            container.RegisterType<C>();
            var h = container.CreateInstance<H>();
            Assert.Equal(aobj, h.C.A);
        }

        [Fact]
        public static void Test30_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            var cobj = new C() { A = aobj, B = bobj };
            container.RegisterType<H>(cobj);
            container.RegisterType<C>();
            var h = container.CreateInstance<H>();
            Assert.Equal(bobj, h.C.B);
        }

        [Fact]
        public static void Test31()
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
            container.RegisterType<H>();
            var h1 = container.GetInstance<H>();
            var h2 = container.GetInstance<H>();
            Assert.NotEqual(h1, h2);
        }

        [Fact]
        public static void Test32()
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
            container.RegisterType<H>();
            var h1 = container.GetInstance<H>();
            var h2 = container.GetInstance<H>();
            Assert.NotEqual(h1.C, h2.C);
        }

        [Fact]
        public static void Test33()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<H>();
            container.RegisterType<C>(aobj, bobj);
            var h = container.GetInstance<H>();
            Assert.NotNull(h.C);
        }

        [Fact]
        public static void Test34_1()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<H>();
            container.RegisterType<C>(aobj, bobj);
            var h = container.GetInstance<H>();
            Assert.Equal(aobj, h.C.A);
        }

        [Fact]
        public static void Test34_2()
        {
            //TestSet23
            var container = new Container(new TypeBuilderOptions
            {
                Activator = new PropertyTypeActivator(),
                Constructor = new PropertyTypeConstructor()
            });
            var aobj = new A();
            var bobj = new B();
            container.RegisterType<H>();
            container.RegisterType<C>(aobj, bobj);
            var h = container.GetInstance<H>();
            Assert.Equal(bobj, h.C.B);
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