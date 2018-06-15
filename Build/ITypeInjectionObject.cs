namespace Build
{
    public interface ITypeInjectionObject
    {
        IInjectionAttribute InjectionAttribute { get; }
        IRuntimeType RuntimeType { get; }
    }
}