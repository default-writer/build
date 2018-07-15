using System;
using System.Collections.Generic;

namespace Build.Tests.Classes
{
    /// <summary>
    /// Injection attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyInjectionAttribute : RuntimeAttribute, IInjectionAttribute
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        readonly object[] _arguments = Array.Empty<object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <param name="args">The arguments.</param>
        public PropertyInjectionAttribute(string typeId, object[] args) : base(typeId) => _arguments = args ?? throw new ArgumentNullException(nameof(args));

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        public PropertyInjectionAttribute(Type type, object[] args) : base(type) => _arguments = args ?? throw new ArgumentNullException(nameof(args));

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        public PropertyInjectionAttribute(string typeId) : base(typeId) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PropertyInjectionAttribute(Type type) : base(type) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        public PropertyInjectionAttribute() { }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Arguments => Format.GetNames(_arguments);

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override RuntimeInstance RuntimeInstance => RuntimeInstance.Exclude;

        /// <summary>
        /// Checks that selected index is within parameters array bounds
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns true if selected index is within parameters array bounds</returns>
        public bool CheckBounds(int index) => index < _arguments.Length;

        /// <summary>
        /// Gets injected object parameters
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns value in array at specified index</returns>
        public object GetObject(int index) => _arguments[index];
    }
}