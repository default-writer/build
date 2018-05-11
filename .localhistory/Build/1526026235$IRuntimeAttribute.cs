using System;

namespace Build
{
    public interface IRuntimeAttribute
    {
        Guid Guid { get; }
        Type Type { get; }
        string RuntimeTypeId { get; }
        RuntimeInstance Runtime { get; }
        void RegisterRuntimeType(IRuntimeType runtimeType);
        IRuntimeType GetRuntimeType(string typeId);
    }
}
