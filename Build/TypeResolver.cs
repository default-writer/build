using System;
using System.Reflection;

namespace Build
{
    class TypeResolver : ITypeResolver
    {
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);

        public string GetTypeFullName(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null && attribute.TypeFullName != null)
                return attribute.TypeFullName;
            return defaultValue;
        }
    }
}