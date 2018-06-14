namespace Build
{
    public interface ITypeBuilder
    {
        ITypeConstructor Constructor { get; }
        ITypeFilter Filter { get; }
        ITypeResolver Resolver { get; }
        ITypeParser Parser { get; }
    }
}