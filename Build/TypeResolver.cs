using System;
using System.Reflection;

namespace Build
{
    class TypeResolver : ITypeResolver
    {
        public string GetName(Type type) => type.FullName;
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);
        public Type GetType(string typeName) => Type.GetType(typeName);
    }
}
