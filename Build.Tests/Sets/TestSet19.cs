using System;

namespace Build.Tests.TestSet19
{
    public interface IFactory<T>
    {
        T GetInstance();
    }

    public class A
    {
    }

    public class B
    {
        public B(Func<A> func) => Func = func;

        public Func<A> Func { get; }
    }

    public class C1
    {
        public C1(A a, B b)
        {
            A = a;
            B = b;
        }

        public A A { get; }
        public B B { get; }
    }

    public class C2
    {
        public C2(Func<A> a, B b)
        {
            A = a;
            B = b;
        }

        public Func<A> A { get; }
        public B B { get; }
    }

    public class C3
    {
        public C3(LazyFactory<A> a, B b)
        {
            A = a.GetInstance();
            B = b;
        }

        public A A { get; }
        public B B { get; }
    }

    public class Class1
    {
    }

    public class Class2
    {
        readonly Func<Class1> _class1;

        public Class2(Func<Class1> class1) => _class1 = class1;
    }

    public class LazyFactory<T> : IFactory<T>
    {
        public LazyFactory(Func<T> func) => Func = func;

        public Func<T> Func { get; }

        public T GetInstance() => Func();
    }
}