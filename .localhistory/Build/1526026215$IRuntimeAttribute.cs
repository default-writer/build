using System;

namespace Build
{
    public interface IRuntimeAttribute
    {
        Guid Guid { get; }
        Type Type { get; }
        IRuntimeType GetRuntimeType(string typeId);
        string RuntimeTypeId { get; }
        RuntimeInstance Runtime { get; }
        void RegisterRuntimeType(IRuntimeType runtimeType);
    }
}
