using System;
using System.Reflection;
using System.Linq;

namespace Build
{
    class TypeResolver : ITypeResolver
    {
        public string GetTypeId(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null)
            {
                if (attribute.Type != null) return attribute.Type.FullName;
                if (attribute.Id != null) return attribute.Id;
            }
            return defaultValue;
        }
        public string GetName(Type type) => type.FullName;
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);
        public string GetTypeFullName(string typeId, Type[] parameterArgs, IRuntimeType runtimeType) => runtimeType == null ? string.Format("{0}({1})", typeId, string.Join(",", parameterArgs.Select(p => p.FullName).ToArray())) : string.Format("{0}({1})", runtimeType.Id, string.Join(",", parameterArgs.Select(p => p.FullName).ToArray()));
    }
}
