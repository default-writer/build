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
        /// Gets the CLR type.
        /// </summary>
        /// <value>The type.</value>
        Type ActivatorType { get; }

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

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>The runtime parameters.</value>
        IEnumerable<IRuntimeType> RuntimeTypes { get; }

        /// <summary>
        /// Gets the type of the assignable.
        /// </summary>
        /// <value>The type of the assignable.</value>
        string Type { get; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        string TypeFullName { get; }

        /// <summary>
        /// Assignable types
        /// </summary>
        IEnumerable<string> Types { get; }

        /// <summary>
        /// Runtime type value
        /// </summary>
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
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object CreateInstance();

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object CreateInstance(object[] args);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object CreateInstance(string[] args);

        /// <summary>
        /// Creates the value instance
        /// </summary>
        /// <returns></returns>
        object CreateValueInstance();

        /// <summary>
        /// Evaluates the runtime instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute, int? i);

        /// <summary>
        /// Gets the value from the specified attribue.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="typeId">Id</param>
        /// <value>Value</value>
        object GetValue(IRuntimeAttribute attribute, string typeId);

        /// <summary>
        /// Gets the value from the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <value>Value</value>
        object GetValue(string typeId);

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        bool RegisterConstructorParameters(object[] args);

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        bool RegisterConstructorParameters(string[] args);

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

        /// <summary>
        /// Sets the value to the specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="typeId">Id</param>
        /// <param name="value">Value</param>
        void SetValue(IRuntimeAttribute attribute, string typeId, object value);

        /// <summary>
        /// Sets the value to the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <param name="value">Value</param>
        void SetValue(string typeId, object value);
    }
}