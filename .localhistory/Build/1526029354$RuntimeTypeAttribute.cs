using System;
using System.Collections.Generic;

namespace Build
{
    public class RuntimeTypeAttribute : Attribute
    {
        IDictionary<string, IRuntimeAttribute> _runtimeTypes = new Dictionary<string, IRuntimeAttribute>();
        public Guid Guid { get; } = Guid.NewGuid();
        public void RegisterRuntimeType(string typeId, IRuntimeAttribute runtimeType) => _runtimeTypes[typeId] = runtimeType;
        public IRuntimeAttribute GetRuntimeType(string typeId) => _runtimeTypes[typeId];
    }
}
