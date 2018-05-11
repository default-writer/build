using System;
using System.Collections.Generic;

namespace Build
{
    public class RuntimeTypeAttribute : Attribute, IRuntimeAttribute
    {
        IDictionary<string, IRuntimeAttribute> _runtimeTypes = new Dictionary<string, IRuntimeAttribute>();
        public Guid Guid { get; } = Guid.NewGuid();
        public abstract RuntimeInstance Runtime { get; }
        public void RegisterRuntimeType(string typeId, IRuntimeAttribute runtimeType) => _runtimeTypes[typeId] = runtimeType;
        public IRuntimeAttribute GetRuntimeType(string typeId)
        {
            if (!_runtimeTypes.ContainsKey(typeId))
            {
                return this;
            }
            return _runtimeTypes[typeId];
        }
    }
}
