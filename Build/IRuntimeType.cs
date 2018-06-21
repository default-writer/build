using System;
using System.Collections.Generic;

namespace Build
{
    /// <summary>
    /// Runtime type interface
    /// </summary>
    public interface IRuntimeType
    {
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        IRuntimeAttribute Attribute { get; }

        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        /// <value>The parameters count.</value>
        int Count { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>The runtime parameters.</value>
        IEnumerable<IRuntimeType> RuntimeTypes { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        Type Type { get; }

        /// <summary>
        /// Gets the type of the assignable.
        /// </summary>
        /// <value>The type of the assignable.</value>
        string TypeDefinition { get; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        string TypeFullName { get; }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified attribute.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="attribute">The attribute.</param>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns></returns>
        object this[IRuntimeAttribute attribute, string typeFullName] { get; set; }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterRuntimeType">Type of the parameter runtime.</param>
        void AddConstructorParameter(IRuntimeType parameterRuntimeType);

        /// <summary>
        /// Determines whether the specified identifier is assignable from type.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// <c>true</c> if the specified identifier is assignable from type, otherwise <c>false</c>.
        /// </returns>
        bool ContainsTypeDefinition(string id);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object CreateInstance(object[] args);

        /// <summary>
        /// Evaluates the runtime instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateRuntimeInstance(IRuntimeType type, IRuntimeAttribute attribute, int? i);

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        bool RegisterConstructorParameters(object[] args);

        /// <summary>
        /// Registers type full name as assignable type
        /// </summary>
        /// <param name="typeFullName"></param>
        void RegisterTypeDefinition(string typeFullName);

        /// <summary>
        /// Sets the runtime instance type
        /// </summary>
        /// <param name="runtimeInstance"></param>
        void SetRuntimeInstance(RuntimeInstance runtimeInstance);
    }
}