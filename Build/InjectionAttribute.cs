using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectionAttribute : RuntimeAttribute, IRuntimeAttribute
    {
        public InjectionAttribute(string runtimeTypeId, params object[] args) : this(runtimeTypeId) => Args = args;

        public InjectionAttribute(Type type, params object[] args) : this(type) => Args = args;

        public InjectionAttribute(string runtimeTypeId) => RuntimeTypeId = runtimeTypeId;

        public InjectionAttribute(Type type) => Type = type;

        public InjectionAttribute()
        {
        }

        public object[] Args { get; } = new object[0];
        public override RuntimeInstance Runtime => RuntimeInstance.None;

        public override string ToString() => string.Format("Injection Guid {0} Type {1} Id {2} Runtime {3}", Guid, Type, RuntimeTypeId, Runtime);
    }
}