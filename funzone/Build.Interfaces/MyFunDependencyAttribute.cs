using System;

namespace Build.Interfaces
{
    /// <summary>
    /// Dependency attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MyFunDependencyAttribute : RuntimeAttribute, IDependencyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="runtimeInstance">The runtime instance.</param>
        public MyFunDependencyAttribute(string typeFullName, RuntimeInstance runtimeInstance) : base(typeFullName) => RuntimeInstance = runtimeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="runtimeInstance">The runtime instance.</param>
        public MyFunDependencyAttribute(Type type, RuntimeInstance runtimeInstance) : base(type) => RuntimeInstance = runtimeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="runtimeInstance">The runtime instance.</param>
        public MyFunDependencyAttribute(RuntimeInstance runtimeInstance) => RuntimeInstance = runtimeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="typeFullName"></param>
        public MyFunDependencyAttribute(string typeFullName) : base(typeFullName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public MyFunDependencyAttribute(Type type) : base(type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyAttribute"/> class.
        /// </summary>
        public MyFunDependencyAttribute()
        {
        }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public override RuntimeInstance RuntimeInstance { get; } = RuntimeInstance.CreateInstance;
    }
}