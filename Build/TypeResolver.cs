using System;
using System.Reflection;

namespace Build
{
    internal class TypeResolver : ITypeResolver
    {
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);

        public string GetTypeFullName(IRuntimeType constructor, string typeFullName, string[] args)
        {
            return string.Format("{0}({1})", constructor == null ? typeFullName : constructor.Type.FullName, string.Join(",", args));
        }

        public string GetTypeId(IRuntimeAttribute attribute, string defaultValue)
        {
            if (defaultValue == default)
                throw new ArgumentNullException(nameof(defaultValue));
            if (attribute != null && attribute.TypeFullName != null)
                return attribute.TypeFullName;
            return defaultValue;
        }
    }
}