namespace Build
{
    public interface ITypeObject
    {
        /// <summary>
        /// Runtime type
        /// </summary>
        IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the object full name
        /// </summary>
        /// <remarks>If runtime attruibute type full name is unknown, then runtime type full name will be used</remarks>
        string TypeFullName { get; }

        /// <summary>
        /// Object identity
        /// </summary>
        string TypeFullNameWithParameters { get; }
    }
}