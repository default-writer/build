using System;
using Build;

namespace Classes
{
    /// <summary>
    /// Dependency attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class InterfaceDependencyAttribute : RuntimeAttribute, IDependencyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="flags">The runtime instance.</param>
        public InterfaceDependencyAttribute(string typeFullName, Flags flags) : base(typeFullName) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="flags">The runtime instance.</param>
        public InterfaceDependencyAttribute(Type type, Flags flags) : base(type) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="flags">The runtime instance.</param>
        public InterfaceDependencyAttribute(Flags flags) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public InterfaceDependencyAttribute(string typeFullName) : base(typeFullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public InterfaceDependencyAttribute(Type type) : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        public InterfaceDependencyAttribute()
        {
        }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override Flags Flags { get; } = Flags.CreateInstance;
    }
}