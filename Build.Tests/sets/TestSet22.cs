namespace Build.Tests.TestSet22
{
    using TestSet;

    [Interface]
    interface IInterfaceSet1
    {
        C this[A a, B b] { get; }
    }
}