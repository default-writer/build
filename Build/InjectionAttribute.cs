using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectionAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        public InjectionAttribute(string runtimeTypeId, params object[] args) : this(runtimeTypeId) => Args = args;

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

        public object[] Args { get; } = new object[0];
        public override RuntimeInstance Runtime => RuntimeInstance.None;
    }
}