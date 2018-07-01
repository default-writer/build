namespace Build
{
    public interface ITypeActivator
    {
        /// <summary>
        /// Creates the instance with type inferenced evaluated arguments.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object CreateInstance(IRuntimeType instance, IRuntimeType type, IRuntimeAttribute attribute);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        object CreateInstance(IRuntimeType instance);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        object CreateValueInstance(IRuntimeType instance);
    }
}