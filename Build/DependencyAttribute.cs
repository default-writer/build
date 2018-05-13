using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class DependencyAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        public DependencyAttribute(string typeFullName, RuntimeInstance runtimeInstance) : base(typeFullName) => RuntimeInstance = runtimeInstance;

        public DependencyAttribute(Type type, RuntimeInstance runtimeInstance) : base(type) => RuntimeInstance = runtimeInstance;

        public DependencyAttribute(RuntimeInstance runtimeInstance) => RuntimeInstance = runtimeInstance;

        public DependencyAttribute(string typeFullName) : base(typeFullName)
        {
        }

        public DependencyAttribute(Type type) : base(type)
        {
        }

        public DependencyAttribute()
        {
        }

        public override RuntimeInstance RuntimeInstance { get; } = RuntimeInstance.CreateInstance;
    }
}