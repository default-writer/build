namespace Build.Tests.TestSet23
{
    public struct D
    {
    }

    public struct F
    {
        public C C { get; set; }
    }

    public class A
    {
    }

    public class B
    {
    }

    public class C
    {
        public A A { get; set; }

        public B B { get; set; }
    }

    public class E
    {
        public D D { get; set; }
    }

    public class G
    {
        public F F { get; set; }
    }

    public class H
    {
        public C C { get; set; }
    }
}