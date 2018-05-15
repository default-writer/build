using System;
using System.Collections.Generic;

namespace Build
{
    public abstract class RuntimeAttribute : Attribute, IRuntimeAttribute
    {
        protected RuntimeAttribute(string typeFullName = default) => TypeFullName = typeFullName;

        protected RuntimeAttribute(Type type) => TypeFullName = type.FullName;

        public Guid Guid { get; } = Guid.NewGuid();

        public abstract RuntimeInstance RuntimeInstance { get; }

        public IDictionary<string, IRuntimeAttribute> RuntimeTypes { get; } = new Dictionary<string, IRuntimeAttribute>();
        public string TypeFullName { get; }

        public IRuntimeAttribute GetRuntimeType(string id)
        {
            if (!RuntimeTypes.ContainsKey(id))
                return this;
            return RuntimeTypes[id];
        }

        public void RegisterRuntimeType(string id, IRuntimeAttribute runtimeType) => RuntimeTypes[id] = runtimeType;
    }
}