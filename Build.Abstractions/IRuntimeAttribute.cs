namespace Build
{
    public interface IRuntimeAttribute
    {
        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        RuntimeInstance RuntimeInstance { get; }

        /// <summary>
        /// Gets the full name of the type.
        /// </summary>
        /// <value>The full name of the type.</value>
        string TypeFullName { get; }

        /// <summary>
        /// Gets the type of the runtime.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <returns></returns>
        IRuntimeAttribute GetReferenceAttribute(string typeId);

        /// <summary>
        /// Registers the type of the runtime.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        void RegisterReferenceAttrubute(string typeId, IRuntimeAttribute attribute, bool defaultTypeAttributeOverwrite);
    }
}