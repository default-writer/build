using System;

namespace Build
{
    class TypeFilter : ITypeFilter
    {
        bool _createFilter(Type type) => type.IsPublic;
        bool _registerFilter(Type type) =>
            !type.IsInterface && !type.IsAbstract && !type.IsValueType && !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);
        public bool CanCreate(Type type) => _createFilter(type);
        public bool CanRegister(Type type) => _registerFilter(type);
    }
}
