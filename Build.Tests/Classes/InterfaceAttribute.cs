using System;

namespace Build.Tests.Classes
{
    /// <summary>
    /// Injection attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class InterfaceAttribute : Attribute
    {
    }
}