namespace Build
{
    public interface ITypeObject
    {
        /// <summary>
        /// Runtime type
        /// </summary>
        IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the type name
        /// </summary>
        /// <remarks>If runtime attruibute type full name is unknown, then runtime type full name will be used</remarks>
        string Type { get; }

        /// <summary>
        /// Gets the type identity (type name with parameters)
        /// </summary>
        string TypeIdentity { get; }
    }
}