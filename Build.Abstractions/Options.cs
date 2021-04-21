using System;

namespace Build
{
    /// <summary>
    /// Runtime instance
    /// </summary>
    [Flags]
    public enum Options

    {
        None = 0x0,
        Exclude = 0x1,
        Singleton = 0x2,
        CreateInstance = 0x4,
        GetInstance = 0x8
    }
}