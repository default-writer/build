using System;
using System.Collections.Generic;

namespace Build
{
    public interface IRuntimeType
    {
        Type AssignableType { get; }

        IRuntimeAttribute Attribute { get; }

        string Id { get; }

        int ParametersCount { get; }

        IEnumerable<IRuntimeType> RuntimeParameters { get; }

        Type Type { get; }

        bool IsAssignableFrom(string id);
    }
}