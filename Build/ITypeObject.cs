namespace Build
{
    public interface ITypeObject
    {
        IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the type full name
        /// </summary>
        string TypeFullName { get; }
    }
}