using System;
using System.Reflection;

namespace Build
{
    class TypeResolver : ITypeResolver
    {
        public string GetName(Type type) => type.AssemblyQualifiedName;
        public Type GetType(Assembly assembly, string typeName) => assembly.GetType(typeName) ?? Type.GetType(typeName);
    }
}
