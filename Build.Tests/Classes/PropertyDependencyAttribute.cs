using System;
using Build;

namespace Classes
{
    /// <summary>
    /// Dependency attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PropertyDependencyAttribute : RuntimeAttribute, IDependencyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="options">The runtime instance.</param>
        public PropertyDependencyAttribute(string typeFullName, Options options) : base(typeFullName) => Options = options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="options">The runtime instance.</param>
        public PropertyDependencyAttribute(Type type, Options options) : base(type) => Options = options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="options">The runtime instance.</param>
        public PropertyDependencyAttribute(Options options) => Options = options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public PropertyDependencyAttribute(string typeFullName) : base(typeFullName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PropertyDependencyAttribute(Type type) : base(type) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        public PropertyDependencyAttribute() { }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override Options Options { get; } = Options.CreateInstance;
    }
}