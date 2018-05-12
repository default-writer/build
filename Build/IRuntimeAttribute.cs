using System;

namespace Build
{
    public interface IRuntimeAttribute
    {
        Guid Guid { get; }
        RuntimeInstance RuntimeInstance { get; }
        string TypeFullName { get; }

        IRuntimeAttribute GetRuntimeType(string typeId);

        void RegisterRuntimeType(string typeId, IRuntimeAttribute attribute);
    }
}