namespace Build
{
    public interface ITypeBuilder
    {
        ITypeConstructor Constructor { get; }
        ITypeFilter Filter { get; }
        ITypeParser Parser { get; }
        ITypeResolver Resolver { get; }
    }
}