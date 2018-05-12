using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        public DependencyAttribute(string typeFullName, RuntimeInstance runtime) : base(typeFullName) => Runtime = runtime;

        public DependencyAttribute(Type type, RuntimeInstance runtime) : base(type) => Runtime = runtime;

        public DependencyAttribute(RuntimeInstance runtime) => Runtime = runtime;

        public DependencyAttribute(string typeFullName) : base(typeFullName) { }

        public DependencyAttribute(Type type) : base(type) { }

        public DependencyAttribute()
        {
        }

        public override RuntimeInstance Runtime { get; } = RuntimeInstance.CreateInstance;
    }
}