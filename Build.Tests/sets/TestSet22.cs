namespace Build.Tests.TestSet22
{
    [Interface]
    interface IInterfaceSet1
    {
        C2 this[A a, B b] { get; }
    }

    public class A
    {
    }

    public class B
    {
    }

    public class C2
    {
        public C2(A a, B b)
        {
            A = a;
            B = b;
        }

        public A A { get; }
        public B B { get; }
    }
}