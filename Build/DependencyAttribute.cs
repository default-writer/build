using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class DependencyAttribute : RuntimeAttribute, IRuntimeAttribute
    {
#pragma warning disable SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

        public DependencyAttribute(string typeFullName, RuntimeInstance runtimeInstance) : base(typeFullName) => RuntimeInstance = runtimeInstance;

#pragma warning restore SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

#pragma warning disable SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

        public DependencyAttribute(Type type, RuntimeInstance runtimeInstance) : base(type) => RuntimeInstance = runtimeInstance;

#pragma warning restore SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

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