using System;

namespace Build
{
    public interface IRuntimeAttribute
    {
        Type Type { get; }
        string Id { get; }
        RuntimeInstance Runtime { get; }
    }
}
