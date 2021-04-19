using System;

namespace Build
{
    /// <summary>
    /// Runtime instance
    /// </summary>
    public enum RuntimeInstance
    {
        Default = 0x0,
        Exclude = 0x1,
        Singleton = 0x2,
        CreateInstance = 0x4,
        GetInstance = 0x8
    }
}