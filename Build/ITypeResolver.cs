using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        string GetTypeId(IRuntimeAttribute runtimeAttribute, string defaultValue);
        string GetName(Type type);
        Type GetType(Assembly assembly, string typeName);
    }
}
