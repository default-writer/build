using System;

namespace Build
{
    public interface IRuntimeType
    {
        IRuntimeAttribute Attribute { get; }
        string Id { get; }
        IRuntimeType[] RuntimeParameters { get; }

        //string RuntimeTypeId { get; }
        Type Type { get; }

        //IRuntimeType Runtime { get; }
        bool IsAssignableFrom(string id);
    }
}