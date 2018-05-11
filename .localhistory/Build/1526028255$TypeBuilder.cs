using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace Build
{
    public class TypeBuilder
    {
        readonly ITypeFilter _typeFilter;
        readonly ITypeResolver _typeResolver;
        readonly ITypeParser _typeParser;
        IDictionary<string, RuntimeType> _types = new Dictionary<string, RuntimeType>();
        RuntimeType this[string typeId, string subtype, RuntimeType type]
        {
            get
            {
                if (!_types.ContainsKey(typeId + subtype))
                    _types.Add(typeId + subtype, type);
                _types[typeId + subtype].RegisterType(type.Type);
                return _types[typeId + subtype];
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
            return CreateInstance(type.FullName, args);
        }
        public object CreateInstance(string typeId, params object[] args)
        {
            var subtype = ":()";
            if (_types.ContainsKey(typeId + subtype))
                return _types[typeId + subtype].CreateInstance(_types[typeId + subtype], args);
            string[] parameterArgs = args == null ? new string[0] : args.Select(p => p == null ? typeof(object).FullName : p.GetType().FullName).ToArray();
            var runtimeType = (RuntimeType)_typeParser.Find(typeId, parameterArgs, _types.Values);
            string typeFullName = _typeResolver.GetTypeFullName(runtimeType, parameterArgs, typeId);
            if (runtimeType != null) return runtimeType.CreateInstance(runtimeType, args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", typeId));
        }
        HashSet<Type> visited = new HashSet<Type>();
        public void RegisterType(Type type) => RegisterTypeId(type);
        void RegisterTypeId(Type type)
        {
            string typeId = type.FullName;
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            visited.Add(type);
            foreach (var constructor in constructors)
            {
                RegisterConstructor(type, constructor);
            }
            visited.Remove(type);
        }
        void RegisterConstructor(Type type, ConstructorInfo constructor)
        {
            var constructorParameters = constructor.GetParameters().Select(p => p.ParameterType.FullName);
            var runtimeType = new RuntimeType(constructor.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(type, RuntimeInstance.CreateInstance), null, type);
            var parameters = constructor.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var subtype = string.Format(":({0})", i);
                var attribute = parameters[i].GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameters[i].ParameterType);
                var parameterType = parameters[i].ParameterType;
                string typeId = _typeResolver.GetTypeId(attribute, parameterType.FullName);
                var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                if (typeId == type.FullName && typeId == parameterType.FullName)
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                string[] parameterArgs = attribute.Args.Select((p) => p == null ? typeof(object).FullName : p.GetType().FullName).ToArray();
                var injectedType = (RuntimeType)_typeParser.Find(typeId, parameterArgs, _types.Values);
                string typeFullName = _typeResolver.GetTypeFullName(injectedType, parameterArgs, typeId);
                if (_typeFilter.CanRegister(parameterType))
                {
                    if (visited.Contains(parameterType))
                        throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", parameterType.FullName));
                    RegisterTypeId(parameterType);
                }
                var parameterRuntimeTypeBase = new RuntimeType(attribute, runtimeType, parameterType, attribute.Args);
                var parameterRuntimeType = this[typeFullName, ":()", parameterRuntimeTypeBase];
                parameterRuntimeType.Attribute.RegisterRuntimeType(subtype, parameterRuntimeTypeBase);
                if (attribute != null && attribute.Args != null && attribute.Args.Length > 0 && attribute.Args.Length == parameterRuntimeType.RuntimeParameters.Length)
                {
                    if (!parameterRuntimeType.RegisterParameters(parameterRuntimeType.Attribute, attribute.Args))
                        throw new TypeRegistrationException(string.Format("{0} is not registered (parameters mismatch)", type.FullName));
                }
                runtimeType.AddParameter(parameterRuntimeType);
                Debug.WriteLine(parameterRuntimeType);
            }
            RegisterConstructorType(constructor, type, runtimeType);
        }
        void RegisterConstructorType(ConstructorInfo constructor, Type type, RuntimeType baseRuntimeType)
        {
            var subtype = ":()";
            var attribute = baseRuntimeType.Attribute;
            string typeId = _typeResolver.GetTypeId(attribute, type.FullName);
            var attributeType = _typeResolver.GetType(type.Assembly, typeId);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            var parameterArgs = constructor.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            var injectedType = (RuntimeType)_typeParser.Find(typeId, parameterArgs, _types.Values);
            string typeFullName = _typeResolver.GetTypeFullName(injectedType, parameterArgs, typeId);
            if (_types.ContainsKey(typeFullName + subtype) && this[typeFullName, subtype, _types[typeFullName + subtype]].IsInitialized)
                return;
            var runtimeType = this[typeFullName, subtype, baseRuntimeType];
            if (runtimeType != null)
            {
                runtimeType.Initialize(attribute.Runtime, type);
                Debug.WriteLine(runtimeType);
            }
        }
    }
}
