using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectionAttribute : Attribute, IRuntimeAttribute
    {
        public Type Type { get; }
        public string Id { get; }
        public object[] Args { get; }
        public RuntimeInstance Runtime => RuntimeInstance.None;
        public InjectionAttribute(string id, params object[] args) : this(id) => Args = args;
        public InjectionAttribute(string id) => Id = id;
        public InjectionAttribute(Type type, params object[] args) : this(type) => Args = args;
        public InjectionAttribute(Type type) => Type = type;
        public InjectionAttribute() { }
    }
}
