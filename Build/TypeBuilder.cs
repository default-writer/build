using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Build
{
    public class TypeBuilder
    {
        readonly ITypeFilter _typeFilter;
        readonly ITypeResolver _typeResolver;
        readonly ITypeParser _typeParser;
        IDictionary<string, RuntimeType> _types = new Dictionary<string, RuntimeType>();
        IDictionary<string, RuntimeType> Types => _types;
        RuntimeType this[string typeId, Type type]
        {
            get
            {
                if (!_types.ContainsKey(typeId))
                    _types.Add(typeId, new RuntimeType(type));
                _types[typeId].RegisterType(type);
                return _types[typeId];
            }
        }
        public bool CanCreate(Type type) => _typeFilter.CanCreate(type);
        public bool CanRegister(Type type) => _typeFilter.CanRegister(type);
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
        public object CreateInstance(Type type, params object[] args)
        {
            string typeId = _typeResolver.GetName(type);
            return CreateInstance(typeId, args);
        }
        public object CreateInstance(string typeId, params object[] args)
        {
            if (_types.ContainsKey(typeId) && this[typeId, _types[typeId].Type].IsRegistered)
                return _types[typeId].CreateInstance(_types[typeId].Type, args);
            var runtimeType = (RuntimeType)_typeParser.Find(typeId, _types.Values);
            if (runtimeType != null) return runtimeType.CreateInstance(runtimeType.Type, args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", typeId));
        }
        HashSet<Type> visited = new HashSet<Type>();
        void RegisterConstructorParameters(Type type)
        {
            if (visited.Contains(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
            RegisterTypeId(type);
        }
        public bool RegisterType(Type type) => RegisterTypeId(type);
        bool RegisterTypeId(Type type)
        {
            string typeId = type.FullName;
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            if (!(_types.ContainsKey(typeId) && this[typeId, type].IsRegistered))
            {
                visited.Add(type);
                foreach (var constructor in constructors)
                {
                    RegisterConstructor(type, constructor);
                }
                visited.Remove(type);
                return true;
            }
            return false;
        }
        void RegisterConstructor(Type type, ConstructorInfo constructor)
        {
            var args = new List<RuntimeType>();
            foreach (var parameterInfo in constructor.GetParameters())
            {
                var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                var parameterType = parameterInfo.ParameterType;
                string typeId = _typeResolver.GetTypeId(attribute, parameterType.FullName);
                var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                if (typeId == type.FullName && typeId == parameterType.FullName)
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                var parameterArgs = new List<Type>();
                if (attribute != null && attribute.Args != null)
                    foreach (var parameter in attribute.Args)
                        parameterArgs.Add(parameter.GetType());
                var runtimeType = (RuntimeType)_typeParser.Find(typeId, _types.Values);
                string typeFullName = _typeResolver.GetTypeFullName(typeId, parameterArgs.ToArray(), runtimeType); //runtimeType == null ? string.Format("{0}({1})", typeId, string.Join(",", parameterArgs.Select(p => p.FullName).ToArray())) : string.Format("{0}({1})", runtimeType.Id, string.Join(",", parameterArgs.Select(p => p.FullName).ToArray()));
                var runtimeParameter = this[typeFullName, parameterType];
                args.Add(runtimeParameter);
                if (parameterArgs.Count > 0 && parameterArgs.Count == runtimeParameter.RuntimeParameters.Length)
                    if (!runtimeParameter.RegisterParameters(type, attribute.Args))
                        throw new TypeRegistrationException(string.Format("{0} is not registered (parameters mismatch)", type.FullName));
                if (_typeFilter.CanRegister(parameterType))
                    RegisterConstructorParameters(parameterType);
            }
            RegisterConstructorType(constructor.GetCustomAttribute<DependencyAttribute>(), type, args);
        }
        void RegisterConstructorType(IRuntimeAttribute attribute, Type type, List<RuntimeType> args)
        {
            string typeId = _typeResolver.GetTypeId(attribute, type.FullName);
            if (attribute != null && _types.ContainsKey(typeId) && this[typeId, type].IsRegistered)
                return;
            var attributeType = _typeResolver.GetType(type.Assembly, typeId);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            var runtimeInstance = attribute == null ? RuntimeInstance.CreateInstance : attribute.Runtime;
            var parameterArgs = new List<Type>(args.Select(p => p.Type));
            var runtimeType = (RuntimeType)_typeParser.Find(typeId, _types.Values);
            string typeFullName = _typeResolver.GetTypeFullName(typeId, parameterArgs.ToArray(), runtimeType);
            if (_types.ContainsKey(typeFullName) && this[typeFullName, type].IsRegistered)
                return;
            this[typeFullName, type].RegisterRuntimeType(runtimeInstance, args.ToArray());
        }
    }
}
