using System;
using System.Reflection;

namespace Build
{
    public class Container : IContainer
    {
        bool _createFilter(Type type) => type.IsPublic;
        bool _registerFilter(Type type) =>
            !type.IsInterface && !type.IsAbstract && !type.IsValueType && !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);

        TypeBuilder typeBuilder = new TypeBuilder();
        public T CreateInstance<T>()
        {
            if (!_createFilter(typeof(T)))
                throw new Exception(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            return (T)typeBuilder.CreateInstance(typeof(T));
        }
        public void RegisterType<T>()
        {
            if (!_registerFilter(typeof(T)))
                throw new Exception(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
            typeBuilder.RegisterType(typeof(T));
        }
        public void RegisterAssemblyTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new Exception(nameof(assembly));
            foreach (var type in assembly.GetTypes())
                if (_registerFilter(type))
                    typeBuilder.RegisterType(type);
        }
    }
}
