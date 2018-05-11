using System;

namespace Build
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class DependencyAttribute : RuntimeTypeAttribute, IRuntimeAttribute
    {
        public Type Type { get; }
        public string RuntimeTypeId { get; }
        public RuntimeInstance Runtime { get; } = RuntimeInstance.CreateInstance;
        public DependencyAttribute(string runtimeTypeId, RuntimeInstance runtime) { RuntimeTypeId = runtimeTypeId; Runtime = runtime; }
        public DependencyAttribute(Type type, RuntimeInstance runtime) { Type = type; Runtime = runtime; }
        public DependencyAttribute(RuntimeInstance runtime) => Runtime = runtime;
        public DependencyAttribute(string runtimeTypeId) => RuntimeTypeId = runtimeTypeId;
        public DependencyAttribute(Type type) => Type = type;
        public DependencyAttribute() { }
        public override string ToString() => string.Format("Guid {0} Type {1} Id {2} Runtime {3}", Guid, Type, RuntimeTypeId, Runtime);
    }
}
