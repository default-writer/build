namespace Build
{
    public interface IContainer
    {
        T CreateInstance<T>();
        void RegisterType<T>();
    }
}
