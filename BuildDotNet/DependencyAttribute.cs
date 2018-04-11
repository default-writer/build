using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class)]
    public class DependencyAttribute : Attribute, IRuntimeAttribute
    {
        public Type Type { get; }
        public string Id { get; }
        public RuntimeInstance Runtime { get; } = RuntimeInstance.CreateInstance;
        public DependencyAttribute(string id, RuntimeInstance runtime) { Id = id; Runtime = runtime; }
        public DependencyAttribute(Type type, RuntimeInstance runtime) { Type = type; Runtime = runtime; }
        public DependencyAttribute(RuntimeInstance runtime) => Runtime = runtime;
        public DependencyAttribute(string id) => Id = id;
        public DependencyAttribute(Type type) => Type = type;
        public DependencyAttribute() { }
    }
}
