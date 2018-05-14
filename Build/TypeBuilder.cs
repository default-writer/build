using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

namespace Build
{
    public class TypeBuilder

    {
        readonly ITypeFilter _typeFilter;
        readonly ITypeParser _typeParser;
        readonly ITypeResolver _typeResolver;
        readonly List<Type> visited = new List<Type>();
        IDictionary<string, RuntimeType> _types = new Dictionary<string, RuntimeType>();

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

        public ITypeFilter Filter => _typeFilter;

        public ITypeParser Parser => _typeParser;

        public IEnumerable<string> RegisteredIds => _types.Keys;

        public ITypeResolver Resolver => _typeResolver;

        internal IEnumerable<RuntimeType> RegisteredTypes => _types.Values;

        RuntimeType this[string id, RuntimeType type]
        {
            get
            {
                if (!_types.ContainsKey(id))
                    _types.Add(id, type);
                _types[id].RegisterType(type.Type);
                return _types[id];
            }
        }

        public bool CanCreate(Type type) => _typeFilter.CanCreate(type);

        public bool CanRegister(Type type) => _typeFilter.CanRegister(type);

        public object CreateInstance(Type type, params object[] args) => CreateInstance(type.FullName, args);

        public object CreateInstance(string id, params object[] args)
        {
            if (_types.ContainsKey(id))
                return _types[id].CreateInstance(args);
            var parameterArgs = GetParameterArgs(args);
            var runtimeType = (RuntimeType)_typeParser.Find(id, parameterArgs, _types.Values);
            if (runtimeType != null) return runtimeType.CreateInstance(args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", id));
        }

        public void RegisterType(Type type) => RegisterTypeId(type);

        static string[] GetInjectedParameterArgs(Type type, Type parameterType, InjectionAttribute injectionAttribute, string id, Type attributeType)
        {
            if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
            if (id == type.FullName && id == parameterType.FullName)
                throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
            return GetParameterArgs(injectionAttribute.Args);
        }

        static string[] GetParameterArgs(object[] args) => args == null ? Array.Empty<string>() : args.Select(p => p == null ? typeof(object).FullName : p.GetType().FullName).ToArray();

        static InjectionAttribute GetParameterAttribute(ParameterInfo parameter) => parameter.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameter.ParameterType);

        static string GetParameterTypeFullName(Type type, ParameterInfo[] parameters) => string.Format("{0}({1})", type.FullName, string.Join(",", parameters.Select(p => p.ParameterType.FullName)));

        string GetTypeId(Type type, IRuntimeAttribute attribute)
        {
            string id = _typeResolver.GetTypeId(attribute, type.FullName);
            var attributeType = _typeResolver.GetType(type.Assembly, id);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            return id;
        }

        void RegisterConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructorInfo in constructors)
            {
                var parameters = constructorInfo.GetParameters();
                var dependencyAttribute = constructorInfo.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(type, RuntimeInstance.CreateInstance);
                var constructor = new RuntimeType(dependencyAttribute, null, type);
                for (int i = 0; i < parameters.Length; i++)
                {
                    RegisterConstructorParameter(i, type, constructor, parameters);
                }
                RegisterConstructorType(constructorInfo, type, constructor);
            }
        }

        void RegisterConstructorParameter(int i, Type type, RuntimeType constructor, ParameterInfo[] parameters)
        {
            var parameterType = parameters[i].ParameterType;
            var injectionAttribute = GetParameterAttribute(parameters[i]);
            var parameter = new RuntimeType(injectionAttribute, constructor, parameters[i].ParameterType, injectionAttribute.Args);
            string id = _typeResolver.GetTypeId(injectionAttribute, parameterType.FullName);
            var attributeType = _typeResolver.GetType(type.Assembly, id);
            var parameterArgs = GetInjectedParameterArgs(type, parameterType, injectionAttribute, id, attributeType);
            var runtimeType = _typeParser.Find(id, parameterArgs, _types.Values);
            if (runtimeType == null)
                RegisterConstructorType(attributeType);
            RegisterConstructorType(parameterType);
            string typeFullName = _typeResolver.GetTypeFullName(runtimeType, id, parameterArgs);
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                result.Attribute.RegisterRuntimeType(string.Format("{0}:({1})", GetParameterTypeFullName(type, parameters), i), injectionAttribute);
                constructor.AddParameter(result);
            }
        }

        void RegisterConstructorType(Type type)
        {
            if (_typeFilter.CanRegister(type))
            {
                if (visited.Contains(type))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                try
                {
                    RegisterTypeId(type);
                }
                catch (TypeRegistrationException ex)
                {
                    throw ex;
                }
            }
        }

        void RegisterConstructorType(ConstructorInfo constructorInfo, Type type, RuntimeType constructor)
        {
            var attribute = constructor.Attribute;
            string id = GetTypeId(type, attribute);
            var parameterArgs = constructorInfo.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            var runtimeType = _typeParser.Find(id, parameterArgs, _types.Values);
            string typeFullName = _typeResolver.GetTypeFullName(runtimeType, id, parameterArgs);
            if (!_types.ContainsKey(typeFullName) || !this[typeFullName, _types[typeFullName]].IsInitialized)
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.Initialize(attribute.RuntimeInstance, type);
                }
            }
        }

        void RegisterTypeId(Type type)
        {
            visited.Add(type);
            RegisterConstructor(type);
            visited.Remove(type);
        }
    }
}