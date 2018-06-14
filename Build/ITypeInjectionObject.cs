namespace Build
{
    public interface ITypeInjectionObject
    {
        IInjectionAttribute InjectionAttribute { get; }
        RuntimeType RuntimeType { get; }
    }
}