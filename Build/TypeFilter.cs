using System;

namespace Build
{
    class TypeFilter : ITypeFilter
    {
        public bool CanCreate(Type type) => type != null;

        public bool CanRegister(Type type) => type != null && type.IsClass && !type.IsAbstract && !IsSpecialType(type);

        static bool IsSpecialType(Type type) => typeof(Type).IsAssignableFrom(type) || typeof(Attribute).IsAssignableFrom(type) || typeof(MarshalByRefObject).IsAssignableFrom(type);
    }
}