using System;
using System.Collections.Generic;

namespace Build
{
    public class RuntimeTypeAttribute : Attribute
    {
        IDictionary<string, IRuntimeType> _runtimeTypes = new Dictionary<string, IRuntimeType>();
        public Guid Guid { get; } = Guid.NewGuid();
        public void RegisterRuntimeType(string typeId, IRuntimeType runtimeType) => _runtimeTypes[this.Guid.ToString()/*typeId*/] = runtimeType;
        public IRuntimeType GetRuntimeType(string typeId) => _runtimeTypes[this.Guid.ToString()];
    }
}
