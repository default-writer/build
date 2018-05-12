using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        Type GetType(Assembly assembly, string typeName);

        string GetTypeFullName(IRuntimeType constructor, string typeFullName, string[] args);

        string GetTypeId(IRuntimeAttribute attribute, string defaultValue);
    }
}