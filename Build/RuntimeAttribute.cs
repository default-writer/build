using System;
using System.Collections.Generic;

namespace Build
{
    public abstract class RuntimeAttribute : Attribute, IRuntimeAttribute
    {
        readonly IDictionary<string, IRuntimeAttribute> _runtimeTypes = new Dictionary<string, IRuntimeAttribute>();

        protected RuntimeAttribute(string typeFullName = default) => TypeFullName = typeFullName;

        protected RuntimeAttribute(Type type) => TypeFullName = type.FullName;

        public Guid Guid { get; } = Guid.NewGuid();

        public abstract RuntimeInstance RuntimeInstance { get; }

        public string TypeFullName { get; }

        public IRuntimeAttribute GetRuntimeType(string id)
        {
            if (!_runtimeTypes.ContainsKey(id))
            {
                return this;
            }
            return _runtimeTypes[id];
        }

        public void RegisterRuntimeType(string id, IRuntimeAttribute runtimeType) => _runtimeTypes[id] = runtimeType;
    }
}