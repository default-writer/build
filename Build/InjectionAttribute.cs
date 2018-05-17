using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InjectionAttribute : RuntimeAttribute, IRuntimeAttribute
    {
#pragma warning disable SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

        public InjectionAttribute(string id, params object[] args) : this(id) => Args = args;

#pragma warning restore SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

#pragma warning disable SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

        public InjectionAttribute(Type type, params object[] args) : this(type) => Args = args;

#pragma warning restore SA1107 // CodeMustNotContainMultipleStatementsOnOneLine

        public InjectionAttribute(string typeFullName) : base(typeFullName)
        {
        }

        public InjectionAttribute(Type type) : base(type)
        {
        }

        public InjectionAttribute()
        {
        }

        public object[] Args { get; } = Array.Empty<object>();
        public override RuntimeInstance RuntimeInstance => RuntimeInstance.None;
    }
}