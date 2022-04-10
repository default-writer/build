#pragma warning disable IDE0039

using System;
using System.Collections.Generic;
using Xunit;
using Build;

namespace TestSet20
{
    public static class UnitTest
    {
        [Fact]
        public static void Method1()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.CreateInstance<Class2>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method10()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv1 = container.CreateInstance<Class2>();
            var srv2 = container.CreateInstance<Class2>();
            Assert.Equal(srv1, srv2);
        }

        [Fact]
        public static void Method11()
        {
            //TestSet2
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv1 = container.GetInstance<Class2>();
            var srv2 = container.GetInstance<Class2>();
            Assert.Equal(srv1, srv2);
        }

        [Fact]
        public static void Method12()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv1 = container.GetInstance<Class2>();
            var srv2 = container.GetInstance<Class2>();
            Assert.Equal(srv1.Func, srv2.Func);
        }

        [Fact]
        public static void Method13()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.GetInstance<Class2>();
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method14()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.GetInstance<Class2>();
            Assert.Null(srv1.Func);
        }

        [Fact]
        public static void Method15()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var srv1 = container.GetInstance<Class2>();
            Assert.Null(srv1.Func);
        }

        [Fact]
        public static void Method16()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            var srv1 = container.GetInstance<LazyFactory<Class1>>();
            Assert.NotNull(srv1.Func);
        }

        [Fact]
        public static void Method17()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv1 = container.GetInstance<Class2>();
            Assert.NotNull(srv1.Func);
        }

        [Fact]
        public static void Method18()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class3>(class1FactoryMethod);
            var srv1 = container.GetInstance<Class3>();
            var srv2 = container.GetInstance<Class3>();
            Assert.NotEqual(srv1, srv2);
        }

        [Fact]
        public static void Method19()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class3>(class1FactoryMethod);
            var srv1 = container.GetInstance<Class3>();
            var srv2 = container.GetInstance<Class3>();
            Assert.Equal(srv1.Func, srv2.Func);
        }

        [Fact]
        public static void Method2()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv2 = container.CreateInstance<Class2>();
            Assert.NotNull(srv2);
        }

        [Fact]
        public static void Method20()
        {
            //TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var srv1 = container.GetInstance<Class2>();
            Assert.Null(srv1.Func);
        }

        [Fact]
        public static void Method21()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var srv1 = container.GetInstance<Class2>(Array.Empty<object>());
            Assert.Null(srv1.Func);
        }

        [Fact]
        public static void Method22()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            var instance = builder.GetInstance(Format.GetConstructor(typeof(Class2).ToString(), args));
            Assert.NotNull(instance);
        }

        [Fact]
        public static void Method23()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            Assert.Throws<TypeInstantiationException>(() => (Class2)builder.GetInstance(Format.GetConstructor(typeof(Class2).ToString(), args), (object[])null));
        }

        [Fact]
        public static void Method24()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            var instance = (Class2)container.GetInstance(Format.GetConstructor(typeof(Class2).ToString(), args), new object[] { class1FactoryMethod });
            Assert.NotNull(instance.Func);
        }

        [Fact]
        public static void Method25()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();

            Func<Class1> appDbContextFactoryFunction = () => new Class1();

            container.RegisterType<LazyFactory<Class1>>(appDbContextFactoryFunction);
            container.RegisterType<Class4>();

            var instance = container.GetInstance(typeof(Class4).ToString());
            Assert.NotNull(instance);
        }

        [Fact]
        public static void Method26()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var instance = (Class2)builder.GetInstance(typeof(Class2));
            Assert.Null(instance.Func);
        }

        [Fact]
        public static void Method27()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            Func<Class1> args = () => null;
            var instance = builder.GetInstance(typeof(Class2).ToString(), new[] { args });
            Assert.NotNull(instance);
        }

        [Fact]
        public static void Method28()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(typeof(Class2).ToString(), new[] { args }));
        }

        [Fact]
        public static void Method29()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            container.RegisterType<Class2>();
            var instance = (Class2)builder.GetInstance(typeof(Class2));
            Assert.Null(instance.Func);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.CreateInstance<Class2>();
            Assert.Null(srv1.Func);
        }

        [Fact]
        public static void Method30()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class5>();
            Func<Class1> args = () => new Class1();
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(typeof(Class5).ToString(), new[] { args }));
        }

        [Fact]
        public static void Method31()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            Func<Class1> args = () => new Class1();
            var srv1 = container.GetInstance<Class2>(args);
            Assert.NotNull(srv1);
        }

        [Fact]
        public static void Method32()
        {
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            container.RegisterType<Class2>();
            Assert.Throws<TypeInstantiationException>(() => (Class2)builder.GetInstance((Type)null, (string[])null));
        }

        [Fact]
        public static void Method33()
        {
            var container = new Container();
            container.RegisterType<Class2>();
            string t = null;
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance(t));
        }

        [Fact]
        public static void Method34()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            var constructorName = Format.GetConstructor(typeof(Class2).ToString(), args);
            container.RegisterType<Class2>();
            container.RegisterType(constructorName, class1FactoryMethod);
            container.Lock();
            var instance = (Class2)container.GetInstance(constructorName, new object[] { class1FactoryMethod });
            Assert.NotNull(instance.Func);
        }

        [Fact]
        public static void Method35()
        {
            //TestSet2
            var container = new Container();
            container.Lock();
            container.Unlock();
            Assert.False(container.IsLocked);
        }

        [Fact]
        public static void Method36()
        {
            //TestSet2
            var container = new Container();
            container.Lock();
            Assert.True(container.IsLocked);
        }

        [Fact]
        public static void Method37()
        {
            var container = new Container();
            container.RegisterType<Class6>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class6>(class1FactoryMethod);
            var srv1 = container.CreateInstance<Class6>();
            Assert.NotNull(srv1.Func);
        }

        [Fact]
        public static void Method38()
        {
            //TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(typeof(Class2).ToString(), new[] { args }));
        }

        [Fact]
        public static void Method39()
        {
            //TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(typeof(Class2).ToString(), new string[] { "System.IntPtr" }));
        }

        [Fact]
        public static void Method4()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv2 = container.CreateInstance<Class2>();
            Assert.NotNull(srv2.Func);
        }

        [Fact]
        public static void Method40()
        {
            //TestSet2
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(typeof(Class2).ToString(), new Type[] { typeof(IntPtr) }));
        }

        [Fact]
        public static void Method41()
        {
            //TestSet2
            var container = new Container();
            Func<Class1> class1FactoryMethod = () => new Class1();
            var constructorName = typeof(Class2).ToString();
            container.RegisterType<Class2>();
            container.RegisterType(constructorName, class1FactoryMethod);
            container.Lock();
            var instance = (Class2)container.GetInstance(constructorName, new object[] { class1FactoryMethod });
            Assert.NotNull(instance.Func);
        }

        [Fact]
        public static void Method42()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance(Format.GetConstructor(typeof(Class2).ToString(), args), (object[])null));
        }

        [Fact]
        public static void Method43()
        {
            //TestSet2
            var container = new Container();
            var builder = (TypeBuilder)container.Builder;
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<LazyFactory<Class1>>(class1FactoryMethod);
            container.RegisterType<Class2>();
            var args = new List<string> { typeof(Func<Class1>).ToString() };
            var instance = (Class2)builder.GetInstance(Format.GetConstructor(typeof(Class2).ToString(), args));
            Assert.Null(instance.Func);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.CreateInstance<Class2>();
            var srv2 = container.CreateInstance<Class2>();
            Assert.Equal(srv1, srv2);
        }

        [Fact]
        public static void Method6()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            var srv1 = container.CreateInstance<Class2>();
            var srv2 = container.CreateInstance<Class2>();
            Assert.Equal(srv1.Func, srv2.Func);
        }

        [Fact]
        public static void Method7()
        {
            //TestSet2
            var container = new Container();
            container.RegisterType<Class1>();
            container.RegisterType<Class2>();
            var srv1 = container.CreateInstance<Class2>();
            var srv2 = container.CreateInstance<Class2>();
            Assert.Equal(srv1.Func, srv2.Func);
        }

        [Fact]
        public static void Method8()
        {
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            Assert.NotNull(container.CreateInstance<Class1>());
        }

        [Fact]
        public static void Method9()
        {
            var container = new Container();
            container.RegisterType<Class1>();
            Func<Class1> class1FactoryMethod = () => new Class1();
            container.RegisterType<Class2>(class1FactoryMethod);
            Assert.NotNull(container.CreateInstance<Class2>());
        }
    }
}