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
        /// True if parameters are set due registration
        /// </summary>
        bool GetInstance { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        bool IsInitialized { get; }

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

        object Value { get; }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterRuntimeType">Type of the parameter runtime.</param>
        void AddConstructorParameter(bool canRegister, IRuntimeType parameterRuntimeType);

        /// <summary>
        /// Determines whether the specified identifier is assignable from type.
        /// </summary>
        /// <param name="typeFullName">The identifier.</param>
        /// <returns>
        /// <c>true</c> if the specified identifier is assignable from type, otherwise <c>false</c>.
        /// </returns>
        bool ContainsTypeDefinition(string typeFullName);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object CreateInstance(params object[] args);

        /// <summary>
        /// Evaluates the runtime instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute, int? i);

        /// <summary>
        /// Gets the specified value from the specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="id">Id</param>
        /// <value>Value</value>
        object GetValue(IRuntimeAttribute attribute, string id);

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        bool RegisterConstructorParameters(params object[] args);

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

        ///// <summary>
        ///// Sets the specified value to the specified attribute.
        ///// </summary>
        ///// <param name="attribute">Attribute</param>
        ///// <param name="id">Id</param>
        ///// <param name="value">Value</param>
        void SetValue(IRuntimeAttribute attribute, string id, object value);
    }
}