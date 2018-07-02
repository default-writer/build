namespace Build.Tests.TestSet
{
    public class A
    {
    }

    public class B
    {
    }

    public class C
    {
        public C(A a, B b)
        {
            A = a;
            B = b;
        }

        public A A { get; }
        public B B { get; }
    }
}