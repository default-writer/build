using System;

namespace Build.Tests.Classes
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
        /// <param name="runtimeInstance">The runtime instance.</param>
        public PropertyDependencyAttribute(string typeFullName, RuntimeInstance runtimeInstance) : base(typeFullName) => RuntimeInstance = runtimeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="runtimeInstance">The runtime instance.</param>
        public PropertyDependencyAttribute(Type type, RuntimeInstance runtimeInstance) : base(type) => RuntimeInstance = runtimeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="runtimeInstance">The runtime instance.</param>
        public PropertyDependencyAttribute(RuntimeInstance runtimeInstance) => RuntimeInstance = runtimeInstance;

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
        public override RuntimeInstance RuntimeInstance { get; } = RuntimeInstance.CreateInstance;
    }
}