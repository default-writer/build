using System;

namespace Build
{
    internal class TypeFilter : ITypeFilter
    {
        private bool _createFilter(Type type) => type.IsPublic;

        private bool _registerFilter(Type type) =>
            !type.IsInterface && !type.IsAbstract && !type.IsValueType && !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);

        public bool CanCreate(Type type) => _createFilter(type);

        public bool CanRegister(Type type) => _registerFilter(type);
    }
}