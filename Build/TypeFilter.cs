using System;

namespace Build
{
    class TypeFilter : ITypeFilter
    {
        public bool CanCreate(Type type) => type != null;

        public bool CanRegister(Type type) => type != null && !type.IsInterface && !type.IsAbstract && !type.IsValueType && !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type);
    }
}