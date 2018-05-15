using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        Type GetType(Assembly assembly, string typeName);

        string GetTypeFullName(IRuntimeAttribute attribute, string defaultValue);
    }
}