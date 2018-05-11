using System;
using System.Collections.Generic;

namespace Build
{
    public class RuntimeTypeAttribute : Attribute
    {
        IDictionary<string, IRuntimeType> _runtimeTypes = new Dictionary<string, IRuntimeType>();
        public void RegisterRuntimeType(string typeId, IRuntimeType runtimeType) => _runtimeTypes[typeId] = runtimeType;
        public IRuntimeType GetRuntimeType(string typeId) => _runtimeTypes[typeId];
    }
}
