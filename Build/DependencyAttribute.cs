using System;

namespace Build
{
    /// <summary>
    /// Dependency attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class DependencyAttribute : RuntimeAttribute, IDependencyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="flags">The runtime instance.</param>
        public DependencyAttribute(string typeFullName, Flags flags) : base(typeFullName) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="flags">The runtime instance.</param>
        public DependencyAttribute(Type type, Flags flags) : base(type) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="flags">The runtime instance.</param>
        public DependencyAttribute(Flags flags) => Flags = flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public DependencyAttribute(string typeFullName) : base(typeFullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DependencyAttribute(Type type) : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        public DependencyAttribute()
        {
        }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override Flags Flags { get; } = Flags.CreateInstance;
    }
}