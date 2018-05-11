using System;
using System.Reflection;

namespace Build
{
    public interface ITypeResolver
    {
        Type GetType(Assembly assembly, string typeName);
        string GetTypeId(IRuntimeAttribute attribute, string defaultValue);
        string GetTypeFullName(IRuntimeType runtimeType, string[] parameterArgs, string typeId);
    }
}
