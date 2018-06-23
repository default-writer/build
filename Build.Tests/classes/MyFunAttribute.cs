using System;

namespace Build.Tests
{
    /// <summary>
    /// Injection attribute
    /// </summary>
    /// <seealso cref="Build.RuntimeAttribute"/>
    /// <seealso cref="Build.IRuntimeAttribute"/>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class MyFunAttribute : Attribute
    {
    }
}