using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Build
{
    class TypeBuilder
    {
        readonly ITypeFilter _typeFilter;
        readonly ITypeResolver _typeResolver;
        readonly ITypeParser _typeParser;
        IDictionary<string, RuntimeType> _types = new Dictionary<string, RuntimeType>();
        internal bool CanCreate(Type type) => _typeFilter.CanCreate(type);
        internal bool CanRegister(Type type) => _typeFilter.CanRegister(type);
        public ITypeFilter Filter => _typeFilter;
        public ITypeResolver Resolver => _typeResolver;
        public ITypeParser Parser => _typeParser;
        public TypeBuilder()
        {
            _typeFilter = new TypeFilter();
            _typeResolver = new TypeResolver();
            _typeParser = new TypeParser();
        }
        public TypeBuilder(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser)
        {
            _typeFilter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            _typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            _typeParser = typeParser ?? throw new ArgumentNullException(nameof(typeParser));
        }
        internal IDictionary<string, RuntimeType> Types => _types;
        RuntimeType this[string typeId, Type type]
        {
            get
            {
                if (!_types.ContainsKey(typeId))
                    _types.Add(typeId, new RuntimeType(type));
                return _types[typeId];
            }
        }



        public object CreateInstance(Type type)
        {
            string id = _typeResolver.GetName(type);
            string typeName = type.FullName;
            if (_types.ContainsKey(id)) return _types[id].CreateInstance();
            throw new Exception(string.Format("{0} is not instantiable (no constructors available)", typeName));
        }
        public object CreateInstance(string id, params object[] args)
        {
            var type = (RuntimeType)_typeParser.Find(id, _types.Values);
            if (type != null) return type.CreateInstance(args);
            throw new Exception(string.Format("{0} is not instantiable (no constructors available)", id));
        }
        HashSet<Type> visited = new HashSet<Type>();
        void RegisterConstructorParameters(Type type)
        {
            if (visited.Contains(type))
                throw new Exception(string.Format("{0} is not registered (circular references found)", type.FullName));
            RegisterTypeId(type);
        }
        public void RegisterType(Type type) => RegisterTypeId(type);
        void RegisterTypeId(Type type)
        {
            string typeId = type.FullName;
            if (!(_types.ContainsKey(typeId) && this[typeId, type].IsRegistered))
            {
                visited.Add(type);
                RegisterConstructor(type);
                visited.Remove(type);
            }
        }
        void RegisterConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new Exception(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructor in constructors)
            {
                var args = new List<RuntimeType>();
                foreach (var parameterInfo in constructor.GetParameters())
                {
                    var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    var parameterType = parameterInfo.ParameterType;
                    string typeId = _typeResolver.GetTypeId(attribute, parameterType.FullName);
                    var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                    if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                        throw new TypeInjectionException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                    if (typeId == type.FullName && typeId == parameterType.FullName)
                        throw new TypeInjectionException(string.Format("{0} is not registered (circular references found)", type.FullName));
                    args.Add(this[typeId, parameterType]);
                    if (_typeFilter.CanRegister(parameterType))
                        RegisterConstructorParameters(parameterType);
                }
                RegisterConstructorType(constructor.GetCustomAttribute<DependencyAttribute>(), type, args.ToArray());
            }
        }
        void RegisterConstructorType(IRuntimeAttribute attribute, Type type, RuntimeType[] args)
        {
            string typeId = _typeResolver.GetTypeId(attribute, type.FullName);
            if (attribute != null && _types.ContainsKey(typeId) && this[typeId, type].IsRegistered)
                return;
            var attributeType = _typeResolver.GetType(type.Assembly, typeId);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeDependencyException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            var runtimeInstance = attribute?.Runtime ?? RuntimeInstance.CreateInstance;
            this[typeId, type].RegisterType(runtimeInstance, args);
        }
    }
}
