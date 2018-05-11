using System;
using System.Reflection;
using System.Linq;

namespace Build
{
    class TypeResolver : ITypeResolver
    {
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);
        public string GetTypeId(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null)
            {
                if (attribute.Type != null) return attribute.Type.FullName;
                if (attribute.RuntimeTypeId != null) return attribute.RuntimeTypeId;
            }
            return defaultValue;
        }
        public string GetTypeFullName(IRuntimeType runtimeType, string[] parameterArgs, string typeId) => string.Format("{0}({1})", runtimeType == null ? typeId : runtimeType.RuntimeTypeId, string.Join(",", parameterArgs));
    }
}
