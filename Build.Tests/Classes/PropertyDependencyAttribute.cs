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
        /// <param name="flags">The runtime instance.</param>
        public PropertyDependencyAttribute(string typeFullName, Flags flags) : base(typeFullName) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="flags">The runtime instance.</param>
        public PropertyDependencyAttribute(Type type, Flags flags) : base(type) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="flags">The runtime instance.</param>
        public PropertyDependencyAttribute(Flags flags) => Flags = flags;

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
        public override Flags Flags { get; } = Flags.CreateInstance;
    }
}