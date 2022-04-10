using System;

namespace Build
{
    /// <summary>
    /// Dependency attribute
    /// </summary>
    /// <seealso cref="RuntimeAttribute"/>
    /// <seealso cref="IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class DependencyAttribute : RuntimeAttribute, IDependencyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="options">The runtime instance.</param>
        public DependencyAttribute(string typeFullName, Options options) : base(typeFullName) => Options = options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="options">The runtime instance.</param>
        public DependencyAttribute(Type type, Options options) : base(type) => Options = options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="options">The runtime instance.</param>
        public DependencyAttribute(Options options) => Options = options;

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
        public override Options Options { get; } = Options.CreateInstance;
    }
}