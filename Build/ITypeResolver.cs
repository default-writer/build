using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        string GetName(Type type);
        Type GetType(Assembly assembly, string typeName);
    }
}
