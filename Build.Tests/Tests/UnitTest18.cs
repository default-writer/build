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
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method10()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method100()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType(typeof(Factory5<EmptyClass>), (Factory2<EmptyClass>)value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(Factory2<EmptyClass>).ToString());
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method101()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")", value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(IFactory<EmptyClass>).ToString());
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method102()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(IFactory<EmptyClass>).ToString());
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method103()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(IFactory<EmptyClass>).ToString());
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method104()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(IFactory<EmptyClass>).ToString());
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method105()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName);
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method106()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method107()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName);
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method108()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(constructorName, value));
        }

        [Fact]
        public static void TestSet18_Method109()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            container.CreateInstance<IntPtrFactory>();
        }

        [Fact]
        public static void TestSet18_Method11()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method110()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var ptr = container.CreateInstance<IntPtrFactory>();
            Assert.Equal(value, ptr.Handle);
        }

        [Fact]
        public static void TestSet18_Method111()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IntPtr>());
        }

        [Fact]
        public static void TestSet18_Method112()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IntPtr>(value));
        }

        [Fact]
        public static void TestSet18_Method113()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString()));
        }

        [Fact]
        public static void TestSet18_Method114()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString(), Array.Empty<string>()));
        }

        [Fact]
        public static void TestSet18_Method115()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((string)null, (string[])null));
        }

        [Fact]
        public static void TestSet18_Method116()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void TestSet18_Method117()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void TestSet18_Method118()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void TestSet18_Method119()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (string[])null));
        }

        [Fact]
        public static void TestSet18_Method12()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method120()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (object[])null));
        }

        [Fact]
        public static void TestSet18_Method121()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType<Factory5<EmptyClass>>();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((string)null, value));
        }

        [Fact]
        public static void TestSet18_Method122()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>), Array.Empty<object>());
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method123()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = new IntPtr(1);
            container.RegisterType<IntPtrFactory>(value);
            var ptr = (IntPtrFactory)builder.GetInstance(typeof(IntPtrFactory), new Type[] { typeof(IntPtr) });
            Assert.Equal(value, ptr.Handle);
        }

        [Fact]
        public static void TestSet18_Method124()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString());
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method125()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method126()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")", value);
            container.RegisterType(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")");
            var instance = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")");
            Assert.Null(instance.Factory);
        }

        [Fact]
        public static void TestSet18_Method127()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", typeof(Func<EmptyClass>));
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method128()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")"));
        }

        [Fact]
        public static void TestSet18_Method129()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(constructorName, value));
        }

        [Fact]
        public static void TestSet18_Method13()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>(obj, func));
        }

        [Fact]
        public static void TestSet18_Method130()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method131()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method132()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", Array.Empty<string>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method133()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            container.Lock();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), Array.Empty<string>());
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method134()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", Array.Empty<string>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method135()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", Array.Empty<Type>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method136()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method137()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", typeof(Func<EmptyClass>));
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method138()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            Assert.NotNull(container.GetInstance(typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void TestSet18_Method139()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", Array.Empty<Type>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void TestSet18_Method14()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method140()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString(), Array.Empty<Type>()));
        }

        [Fact]
        public static void TestSet18_Method141()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((Type)null, Array.Empty<Type>()));
        }

        [Fact]
        public static void TestSet18_Method142()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var factory = (IntPtrFactory)container.GetInstance(typeof(IntPtrFactory), Array.Empty<Type>());
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method143()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var factory = (IntPtrFactory)container.GetInstance(typeof(IntPtrFactory), Array.Empty<Type>());
            Assert.Equal(value, factory.Handle);
        }

        [Fact]
        public static void TestSet18_Method144()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<Factory2<EmptyClass>>();
            var value = container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType(typeof(Factory5<EmptyClass>), (Factory2<EmptyClass>)value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(Factory2<EmptyClass>).ToString());
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method145()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.RegisterType<Factory2<EmptyClass>>();
            var value = container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType<Factory5<EmptyClass>>((Factory2<EmptyClass>)value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(Factory2<EmptyClass>).ToString());
            Assert.Equal(value, sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method146()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet18_Method147()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet18_Method148()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(MarshalByRefObject), Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet18_Method149()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")", value);
            var instance = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")");
            Assert.NotNull(instance.Factory);
        }

        [Fact]
        public static void TestSet18_Method15()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method150()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, typeof(Func<EmptyClass>));
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName);
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method151()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory5<EmptyClass>>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, typeof(Func<EmptyClass>).ToString());
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName);
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method152()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory5<EmptyClass>));
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, typeof(Func<EmptyClass>).ToString());
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName);
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method153()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory5<EmptyClass>));
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            var constructorName = typeof(Factory5<EmptyClass>);
            container.RegisterType(constructorName, value);
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(constructorName, value);
            Assert.NotNull(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method154()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            container.RegisterType(typeof(Factory5<EmptyClass>));
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((string)null, value));
        }

        [Fact]
        public static void TestSet18_Method155()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType(typeof(Factory5<EmptyClass>));
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory5<EmptyClass>).ToString(), (object[])null));
        }

        [Fact]
        public static void TestSet18_Method156()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ErrorValueClass<int>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ErrorValueClass<int>).ToString(), (ValueClass<int>)null));
        }

        [Fact]
        public static void TestSet18_Method157()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ErrorValueStruct<int>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ErrorValueStruct<int>).ToString()));
        }

        [Fact]
        public static void TestSet18_Method16()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method17()
        {
            //TestSet18
            var container = new Container();
            object obj = null;
            try
            {
                container.RegisterType<Factory2<EmptyClass>>(obj);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public static void TestSet18_Method18()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Null(factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method19()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method2()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method20()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method21()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), (object)(func));
            var factory = (Factory3<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method22()
        {
            //TestSet18
            var container = new Container();
            object obj = new object();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { obj }));
        }

        [Fact]
        public static void TestSet18_Method23()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            var factory = (Factory3<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method25()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method26()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), (object)func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method27()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method28()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.EmptyClass](System.Object)"));
        }

        [Fact]
        public static void TestSet18_Method29()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method3()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(LazyFactory<EmptyClass>), new object[] { func });
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method30()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (IFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method31()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method32()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory1 = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            var factory2 = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(factory1.Func, factory2.Func);
        }

        [Fact]
        public static void TestSet18_Method33()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var count = RuntimeTypeExtensions.FindRuntimeTypes(((TypeBuilder)container.Builder).Types.Values, "Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])").Length;
            Assert.Equal(1, count);
        }

        [Fact]
        public static void TestSet18_Method34()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>(obj));
        }

        [Fact]
        public static void TestSet18_Method35()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            var factory = (Factory3<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method36()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]", (Func<EmptyClass>)null);
            Assert.NotNull(factory);
        }

        [Fact]
        public static void TestSet18_Method37()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory3<EmptyClass>>(func);
            var factory = (Factory3<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory3`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method38()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), func);
            var factory = (Factory3<EmptyClass>)container.CreateInstance(typeof(Factory3<EmptyClass>).ToString());
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method39()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method4()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.LazyFactory`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void TestSet18_Method40()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method41()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method42()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method43()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>(typeof(Func<EmptyClass>));
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method44()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void TestSet18_Method45()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.GetInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])", new object[] { func });
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method46()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>();
            var factory = (Factory4<EmptyClass>)container.GetInstance("Build.Tests.TestSet18.Factory4`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])", new object[] { func });
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method47()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.GetInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method48()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>(func);
            var factory = (Factory4<EmptyClass>)container.GetInstance("Build.Tests.TestSet18.Factory4`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method49()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>(func);
            var factory = (Factory4<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory4`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method5()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method50()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>((Type)null));
        }

        [Fact]
        public static void TestSet18_Method51()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (Type)null));
        }

        [Fact]
        public static void TestSet18_Method52()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void TestSet18_Method53()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(IntPtr), typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void TestSet18_Method54()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Func<EmptyClass>), typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void TestSet18_Method55()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (string)null));
        }

        [Fact]
        public static void TestSet18_Method56()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (string)null));
        }

        [Fact]
        public static void TestSet18_Method57()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void TestSet18_Method58()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(IntPtr), typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void TestSet18_Method59()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Func<EmptyClass>), typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void TestSet18_Method6()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method60()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method61()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method62()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method63()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(func);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method64()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance((string)null, typeof(Func<EmptyClass>)));
        }

        [Fact]
        public static void TestSet18_Method65()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void TestSet18_Method66()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method67()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.GetInstance(typeof(ValueStruct<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method68()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<int>(1);
            int value = (int)container.GetInstance(typeof(int).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method69()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            int value = (ValueClass<int>)container.GetInstance(typeof(ValueClass<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method7()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method70()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            int value = (ValueClass<int>)container.CreateInstance(typeof(ValueClass<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method71()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>();
            int value = (ValueClass<int>)container.CreateInstance(typeof(ValueClass<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method72()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>();
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method73()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<int>(1);
            int value = (int)container.CreateInstance(typeof(int).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method74()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1));
        }

        [Fact]
        public static void TestSet18_Method75()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            container.RegisterType<ValueClass<int>>(1);
            var value = container.CreateInstance<ValueClass<int>>();
            Assert.Equal(1, value.Value);
        }

        [Fact]
        public static void TestSet18_Method76()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void TestSet18_Method77()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            var value = container.CreateInstance<ValueClass<int>>();
            Assert.Equal(1, value.Value);
        }

        [Fact]
        public static void TestSet18_Method78()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void TestSet18_Method79()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false, UseDefaultConstructor = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void TestSet18_Method8()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method80()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false, UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1));
        }

        [Fact]
        public static void TestSet18_Method81()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1, 2, 3));
        }

        [Fact]
        public static void TestSet18_Method82()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1, 2, 3));
        }

        [Fact]
        public static void TestSet18_Method83()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance((Type)null, typeof(Func<EmptyClass>).ToString()));
        }

        [Fact]
        public static void TestSet18_Method84()
        {
            //TestSet18
            var container = new Container();
            try
            {
                container.RegisterType(typeof(Func<EmptyClass>), (string[])null);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public static void TestSet18_Method85()
        {
            //TestSet18
            var container = new Container();
            try
            {
                container.RegisterType(typeof(Func<EmptyClass>), (Type[])null);
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public static void TestSet18_Method86()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, (Type[])null));
        }

        [Fact]
        public static void TestSet18_Method87()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.Lock();
            var sql = container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.NotNull(sql);
        }

        [Fact]
        public static void TestSet18_Method88()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.NotNull(sql.Func);
        }

        [Fact]
        public static void TestSet18_Method89()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method9()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass](System.Func`1[Build.Tests.TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method90()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", typeof(Func<EmptyClass>));
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method91()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var value = container.GetInstance<Factory2<EmptyClass>>();
            container.RegisterType(typeof(Factory5<EmptyClass>), (IFactory<EmptyClass>)value);
            container.Lock();
            var sql = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, sql.Func);
        }

        [Fact]
        public static void TestSet18_Method92()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            Assert.NotNull(container.CreateInstance(typeof(Factory2<EmptyClass>), Array.Empty<object>()));
        }

        [Fact]
        public static void TestSet18_Method93()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("Build.Tests.TestSet18.Factory2`1[Build.Tests.TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method94()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.GetInstance<Factory2<EmptyClass>>();
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void TestSet18_Method95()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>), func);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void TestSet18_Method96()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>(func);
            container.RegisterType<Factory5<EmptyClass>>();
            container.Lock();
            var sql = (Factory5<EmptyClass>)container.GetInstance(typeof(Factory5<EmptyClass>), typeof(IFactory<EmptyClass>).ToString());
            Assert.Null(sql.Factory);
        }

        [Fact]
        public static void TestSet18_Method97()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var function = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, function.Func);
        }

        [Fact]
        public static void TestSet18_Method98()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var function = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, function.Func);
        }

        [Fact]
        public static void TestSet18_Method99()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }
    }
}