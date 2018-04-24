using System;
using System.Reflection;

namespace Build
{
    public class Container : IContainer
    {
        TypeBuilder _typeBuilder;
        public Container() => _typeBuilder = new TypeBuilder();
        public Container(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser) => _typeBuilder = new TypeBuilder(typeFilter, typeResolver, typeParser);
        public T CreateInstance<T>()
        {
            if (_typeBuilder.CanCreate(typeof(T)))
                return (T)_typeBuilder.CreateInstance(typeof(T));
            throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
        }
        public void RegisterType<T>()
        {
            if (_typeBuilder.CanRegister(typeof(T)))
            {
                _typeBuilder.RegisterType(typeof(T));
                return;
            }
            throw new TypeFilterException(string.Format("{0} is not instantiable (not an allowed type)", typeof(T).FullName));
        }
        public void RegisterAssemblyTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            foreach (var type in assembly.GetTypes())
                if (_typeBuilder.CanRegister(type))
                    _typeBuilder.RegisterType(type);
        }
        public object CreateInstance(string id, params object[] args)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            return _typeBuilder.CreateInstance(id, args);
        }
    }
}
