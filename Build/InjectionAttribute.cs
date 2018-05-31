using System;

namespace Build
{
    /// <summary>
    /// Injection attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InjectionAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        public InjectionAttribute(string id, params object[] args) : this(id) => Arguments = args;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        public InjectionAttribute(Type type, params object[] args) : this(type) => Arguments = args;

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public InjectionAttribute(string typeFullName) : base(typeFullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public InjectionAttribute(Type type) : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionAttribute"/> class.
        /// </summary>
        public InjectionAttribute()
        {
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public object[] Arguments { get; } = Array.Empty<object>();

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override RuntimeInstance RuntimeInstance => RuntimeInstance.None;
    }
}