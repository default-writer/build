using TestSet18;
using System;
using Xunit;
using Build;

namespace UnitTests18
{
    public static class UnitTests
    {
        [Fact]
        public static void Method1()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method10()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void Method100()
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
        public static void Method101()
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
        public static void Method102()
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
        public static void Method103()
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
        public static void Method104()
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
        public static void Method105()
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
        public static void Method106()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method107()
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
        public static void Method108()
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
        public static void Method109()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.CreateInstance<IntPtrFactory>());
        }

        [Fact]
        public static void Method11()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method110()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var ptr = container.CreateInstance<IntPtrFactory>();
            Assert.Equal(value, ptr.Handle);
        }

        [Fact]
        public static void Method111()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IntPtr>());
        }

        [Fact]
        public static void Method112()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<IntPtr>(value));
        }

        [Fact]
        public static void Method113()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString()));
        }

        [Fact]
        public static void Method114()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString(), ArrayExtensions.Empty<string>()));
        }

        [Fact]
        public static void Method115()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((string)null, (string[])null));
        }

        [Fact]
        public static void Method116()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void Method117()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.GetInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void Method118()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (Type[])null));
        }

        [Fact]
        public static void Method119()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (string[])null));
        }

        [Fact]
        public static void Method12()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void Method120()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var builder = (TypeBuilder)container.Builder;
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => builder.CreateInstance((Type)null, (object[])null));
        }

        [Fact]
        public static void Method121()
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
        public static void Method122()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>), ArrayExtensions.Empty<object>());
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method123()
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
        public static void Method124()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString());
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method125()
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
        public static void Method126()
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
        public static void Method127()
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
        public static void Method128()
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
        public static void Method129()
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
        public static void Method13()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>(obj, func));
        }

        [Fact]
        public static void Method130()
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
        public static void Method131()
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
        public static void Method132()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", ArrayExtensions.Empty<string>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void Method133()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            container.Lock();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), ArrayExtensions.Empty<string>());
            Assert.NotNull(value);
        }

        [Fact]
        public static void Method134()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", ArrayExtensions.Empty<string>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void Method135()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", ArrayExtensions.Empty<Type>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void Method136()
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
        public static void Method137()
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
        public static void Method138()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            Assert.NotNull(container.GetInstance(typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void Method139()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType<IntPtrFactory>();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType<Factory5<EmptyClass>>();
            var value = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")", ArrayExtensions.Empty<Type>());
            var constructorName = typeof(Factory5<EmptyClass>) + "(" + typeof(IFactory<EmptyClass>) + ")";
            container.RegisterType(constructorName, value);
            Assert.NotNull(value);
        }

        [Fact]
        public static void Method14()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method140()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.NotNull(container.GetInstance(typeof(IntPtrFactory).ToString(), ArrayExtensions.Empty<Type>()));
        }

        [Fact]
        public static void Method141()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance((Type)null, ArrayExtensions.Empty<Type>()));
        }

        [Fact]
        public static void Method142()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var factory = (IntPtrFactory)container.GetInstance(typeof(IntPtrFactory), ArrayExtensions.Empty<Type>());
            Assert.NotNull(factory);
        }

        [Fact]
        public static void Method143()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            var value = IntPtr.Zero;
            container.RegisterType<IntPtrFactory>(value);
            var factory = (IntPtrFactory)container.GetInstance(typeof(IntPtrFactory), ArrayExtensions.Empty<Type>());
            Assert.Equal(value, factory.Handle);
        }

        [Fact]
        public static void Method144()
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
        public static void Method145()
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
        public static void Method146()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, ArrayExtensions.Empty<object>()));
        }

        [Fact]
        public static void Method147()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), ArrayExtensions.Empty<object>()));
        }

        [Fact]
        public static void Method148()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(MarshalByRefObject), ArrayExtensions.Empty<object>()));
        }

        [Fact]
        public static void Method149()
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
        public static void Method15()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method150()
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
        public static void Method151()
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
        public static void Method152()
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
        public static void Method153()
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
        public static void Method154()
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
        public static void Method155()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            container.RegisterType(typeof(Factory5<EmptyClass>));
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory5<EmptyClass>).ToString(), (object[])null));
        }

        [Fact]
        public static void Method156()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ErrorValueClass<int>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ErrorValueClass<int>).ToString(), (ValueClass<int>)null));
        }

        [Fact]
        public static void Method157()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ErrorValueStruct<int>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance(typeof(ErrorValueStruct<int>).ToString()));
        }

        [Fact]
        public static void Method16()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method17()
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
        public static void Method18()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Null(factory.GetInstance());
        }

        [Fact]
        public static void Method19()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void Method2()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method20()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func }));
        }

        [Fact]
        public static void Method21()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            static EmptyClass func() => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), (object)(Func<EmptyClass>)func);
            var factory = (Factory3<EmptyClass>)container.CreateInstance("TestSet18.Factory3`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void Method22()
        {
            //TestSet18
            var container = new Container();
            object obj = new();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { obj }));
        }

        [Fact]
        public static void Method23()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            var factory = (Factory3<EmptyClass>)container.CreateInstance("TestSet18.Factory3`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory);
        }

        [Fact]
        public static void Method25()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method26()
        {
            //TestSet18
            var container = new Container();
            static EmptyClass func() => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), (object)(Func<EmptyClass>)func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method27()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method28()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions { UseDefaultTypeAttributeOverwrite = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("TestSet18.Factory3`1[TestSet18.EmptyClass](System.Object)"));
        }

        [Fact]
        public static void Method29()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>) + "(" + typeof(Func<EmptyClass>) + ")");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method3()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(LazyFactory<EmptyClass>), new object[] { func });
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method30()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (IFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void Method31()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method32()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var factory1 = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            var factory2 = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(factory1.Func, factory2.Func);
        }

        [Fact]
        public static void Method33()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<LazyFactory<EmptyClass>>(func);
            var count = RuntimeTypeExtensions.FindRuntimeTypes(((TypeBuilder)container.Builder).Types.Values, "TestSet18.LazyFactory`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])").Length;
            Assert.Equal(1, count);
        }

        [Fact]
        public static void Method34()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            object obj = null;
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>(obj));
        }

        [Fact]
        public static void Method35()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), new object[] { func });
            var factory = (Factory3<EmptyClass>)container.CreateInstance("TestSet18.Factory3`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method36()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]", (Func<EmptyClass>)null);
            Assert.NotNull(factory);
        }

        [Fact]
        public static void Method37()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory3<EmptyClass>>(func);
            var factory = (Factory3<EmptyClass>)container.CreateInstance("TestSet18.Factory3`1[TestSet18.EmptyClass]");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method38()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory3<EmptyClass>), func);
            var factory = (Factory3<EmptyClass>)container.CreateInstance(typeof(Factory3<EmptyClass>).ToString());
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method39()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method4()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance("TestSet18.LazyFactory`1[TestSet18.EmptyClass]");
            Assert.Equal(type, factory.GetInstance());
        }

        [Fact]
        public static void Method40()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method41()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(LazyFactory<EmptyClass>), func);
            var factory = (LazyFactory<EmptyClass>)container.CreateInstance(typeof(LazyFactory<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method42()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method43()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>(typeof(Func<EmptyClass>));
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void Method44()
        {
            //TestSet18
            var container = new Container();
            Func<object> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            Assert.Throws<TypeInstantiationException>(() => container.GetInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])", new object[] { func }));
        }

        [Fact]
        public static void Method45()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.GetInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])", new object[] { func });
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method46()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>();
            var factory = (Factory4<EmptyClass>)container.GetInstance("TestSet18.Factory4`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])", new object[] { func });
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method47()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.GetInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method48()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>(func);
            var factory = (Factory4<EmptyClass>)container.GetInstance("TestSet18.Factory4`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method49()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType<Factory4<EmptyClass>>(func);
            var factory = (Factory4<EmptyClass>)container.CreateInstance("TestSet18.Factory4`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method5()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]()");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method50()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<Factory2<EmptyClass>>((Type)null));
        }

        [Fact]
        public static void Method51()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (Type)null));
        }

        [Fact]
        public static void Method52()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void Method53()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(IntPtr), typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void Method54()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Func<EmptyClass>), typeof(Factory2<EmptyClass>)));
        }

        [Fact]
        public static void Method55()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (string)null));
        }

        [Fact]
        public static void Method56()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Factory2<EmptyClass>), (string)null));
        }

        [Fact]
        public static void Method57()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void Method58()
        {
            //TestSet18
            var container = new Container();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(IntPtr), typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void Method59()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType(typeof(Func<EmptyClass>), typeof(Factory2<EmptyClass>).ToString()));
        }

        [Fact]
        public static void Method6()
        {
            //TestSet18
            var container = new Container();
            var type = new EmptyClass();
            Func<EmptyClass> func = () => type;
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method60()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method61()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>));
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method62()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>).ToString(), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method63()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.CreateInstance<Factory2<EmptyClass>>(func);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method64()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance((string)null, typeof(Func<EmptyClass>)));
        }

        [Fact]
        public static void Method65()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false, UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void Method66()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method67()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.GetInstance(typeof(ValueStruct<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method68()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<int>(1);
            int value = (int)container.GetInstance(typeof(int).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method69()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            int value = (ValueClass<int>)container.GetInstance(typeof(ValueClass<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method7()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method70()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            int value = (ValueClass<int>)container.CreateInstance(typeof(ValueClass<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method71()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>();
            int value = (ValueClass<int>)container.CreateInstance(typeof(ValueClass<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method72()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>();
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString(), 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method73()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<int>(1);
            int value = (int)container.CreateInstance(typeof(int).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method74()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1));
        }

        [Fact]
        public static void Method75()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            container.RegisterType<ValueClass<int>>(1);
            var value = container.CreateInstance<ValueClass<int>>();
            Assert.Equal(1, value.Value);
        }

        [Fact]
        public static void Method76()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueStruct<int>>(1);
            int value = (ValueStruct<int>)container.CreateInstance(typeof(ValueStruct<int>).ToString());
            Assert.Equal(1, value);
        }

        [Fact]
        public static void Method77()
        {
            //TestSet18
            var container = new Container();
            container.RegisterType<ValueClass<int>>(1);
            var value = container.CreateInstance<ValueClass<int>>();
            Assert.Equal(1, value.Value);
        }

        [Fact]
        public static void Method78()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void Method79()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false, UseDefaultConstructor = false });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1));
        }

        [Fact]
        public static void Method8()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), new object[] { func });
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.NotNull(factory.Func);
        }

        [Fact]
        public static void Method80()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = false, UseDefaultConstructor = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1));
        }

        [Fact]
        public static void Method81()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<ValueStruct<int>>(1, 2, 3));
        }

        [Fact]
        public static void Method82()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseValueTypes = true });
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<int>(1, 2, 3));
        }

        [Fact]
        public static void Method83()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance((Type)null, typeof(Func<EmptyClass>).ToString()));
        }

        [Fact]
        public static void Method84()
        {
            //TestSet18
            var container = new Container();
            try
            {
                container.RegisterType(typeof(Func<EmptyClass>), (string[])null);
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public static void Method85()
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
        public static void Method86()
        {
            //TestSet18
            var container = new Container();
            container.Lock();
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType((Type)null, (Type[])null));
        }

        [Fact]
        public static void Method87()
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
        public static void Method88()
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
        public static void Method89()
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
        public static void Method9()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass](System.Func`1[TestSet18.EmptyClass])");
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method90()
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
        public static void Method91()
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
        public static void Method92()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>(func);
            Assert.NotNull(container.CreateInstance(typeof(Factory2<EmptyClass>), ArrayExtensions.Empty<object>()));
        }

        [Fact]
        public static void Method93()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = (Factory2<EmptyClass>)container.CreateInstance("TestSet18.Factory2`1[TestSet18.EmptyClass]");
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method94()
        {
            //TestSet18
            var container = new Container();
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var factory = container.GetInstance<Factory2<EmptyClass>>();
            Assert.Null(factory.Func);
        }

        [Fact]
        public static void Method95()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => new EmptyClass();
            container.RegisterType<Factory2<EmptyClass>>();
            var factory = (Factory2<EmptyClass>)container.CreateInstance(typeof(Factory2<EmptyClass>), func);
            Assert.Equal(func, factory.Func);
        }

        [Fact]
        public static void Method96()
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
        public static void Method97()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = true });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var function = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, function.Func);
        }

        [Fact]
        public static void Method98()
        {
            //TestSet18
            var container = new Container(new TypeBuilderOptions() { UseDefaultConstructor = false });
            Func<EmptyClass> func = () => null;
            container.RegisterType(typeof(Factory2<EmptyClass>), func);
            var function = (Factory2<EmptyClass>)container.GetInstance(typeof(Factory2<EmptyClass>), typeof(Func<EmptyClass>).ToString());
            Assert.Equal(func, function.Func);
        }

        [Fact]
        public static void Method99()
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