using System;
using System.Collections.Generic;

namespace Build
{
    public abstract class RuntimeAttribute : Attribute, IRuntimeAttribute
    {
        private IDictionary<string, IRuntimeAttribute> _runtimeTypes = new Dictionary<string, IRuntimeAttribute>();
        public Guid Guid { get; } = Guid.NewGuid();
        public abstract RuntimeInstance RuntimeInstance { get; }
        public string TypeFullName { get; }

        protected RuntimeAttribute(string typeFullName = default) => TypeFullName = typeFullName;

        protected RuntimeAttribute(Type type) => TypeFullName = type.FullName;

        public IRuntimeAttribute GetRuntimeType(string typeId)
        {
            if (!_runtimeTypes.ContainsKey(typeId))
            {
                return this;
            }
            return _runtimeTypes[typeId];
        }

        public void RegisterRuntimeType(string typeId, IRuntimeAttribute runtimeType) => _runtimeTypes[typeId] = runtimeType;
    }
}