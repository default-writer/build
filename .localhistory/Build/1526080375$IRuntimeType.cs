using System;

namespace Build
{
    public interface IRuntimeType
    {
        string Id { get; }
        IRuntimeAttribute Attribute { get; }

        //string RuntimeTypeId { get; }
        Type Type { get; }

        IRuntimeType[] RuntimeParameters { get; }

        //IRuntimeType Runtime { get; }
        bool IsAssignableFrom(string typeId);
    }
}