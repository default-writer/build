using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class InjectionAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        public InjectionAttribute(string id, params object[] args) : this(id) => Args = args;

        public InjectionAttribute(Type type, params object[] args) : this(type) => Args = args;

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