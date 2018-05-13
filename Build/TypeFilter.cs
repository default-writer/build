using System;

namespace Build
{
    internal class TypeFilter : ITypeFilter
    {
        public bool CanCreate(Type type) => type != null && _createFilter(type);

        public bool CanRegister(Type type) => type != null && _registerFilter(type);

        bool _createFilter(Type type) => type.IsPublic;

        bool _registerFilter(Type type) =>
            !type.IsInterface && !type.IsAbstract && !type.IsValueType && !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);
    }
}