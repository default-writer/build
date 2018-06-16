using System;
using System.Collections.Generic;
using System.Text;

namespace Build.Interfaces
{
    /// <summary>
    /// Injection attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class MyFunInjectionAttribute : RuntimeAttribute, IInjectionAttribute
    {
        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        readonly object[] _arguments = Array.Empty<object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        public MyFunInjectionAttribute(string id, params object[] args) : this(id) => _arguments = args;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        public MyFunInjectionAttribute(Type type, params object[] args) : this(type) => _arguments = args;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public MyFunInjectionAttribute(string typeFullName) : base(typeFullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public MyFunInjectionAttribute(Type type) : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        public MyFunInjectionAttribute()
        {
        }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Arguments => Format.GetParametersFullName(_arguments);

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override RuntimeInstance RuntimeInstance => RuntimeInstance.None;

        /// <summary>
        /// Checks that selected index is within parameters array bounds
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns true if selected index is within parameters array bounds</returns>
        public bool CheckBounds(int index) => index >= 0 && index < _arguments.Length;

        /// <summary>
        /// Gets injected object parameters
        /// </summary>
        /// <param name="index">Value index in parameters array</param>
        /// <returns>Returns value in array at specified index</returns>
        public object GetObject(int index) => _arguments[index];
    }
}