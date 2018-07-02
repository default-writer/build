namespace Build.Tests.TestSet22
{
    using TestSet;
    using Classes;

    [Interface]
    interface IInterfaceSet1
    {
        C this[A a, B b] { get; }
    }
}