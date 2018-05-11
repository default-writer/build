using System;

namespace Build
{
    public class RuntimeTypeAttribute : Attribute
    {
        IRuntimeType _runtimeType;
        public IRuntimeType RuntimeType => _runtimeType;
        public void RegisterRuntimeType(IRuntimeType runtimeType) => _runtimeType = runtimeType;
    }
}
