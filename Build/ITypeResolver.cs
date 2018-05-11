using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        Type GetType(Assembly assembly, string typeName);

        string GetTypeFullName(IRuntimeType runtimeType, string[] parameterArgs, string typeId);

        string GetTypeId(IRuntimeAttribute attribute, string defaultValue);
    }
}