using System;
using System.Reflection;

namespace Build
{
    internal class TypeResolver : ITypeResolver
    {
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);

        public string GetTypeFullName(IRuntimeType runtimeType, string[] parameterArgs, string typeId) => string.Format("{0}({1})", runtimeType == null ? typeId : runtimeType.Type.FullName, string.Join(",", parameterArgs));

        public string GetTypeId(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null)
            {
                if (attribute.TypeFullName != null) return attribute.TypeFullName;
            }
            return defaultValue;
        }
    }
}