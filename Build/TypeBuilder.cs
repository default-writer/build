using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

namespace Build
{
    public class TypeBuilder

    {
        public TypeBuilder()
        {
            Filter = new TypeFilter();
            Resolver = new TypeResolver();
            Parser = new TypeParser();
        }

        public TypeBuilder(ITypeFilter typeFilter, ITypeResolver typeResolver, ITypeParser typeParser)
        {
            Filter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            Resolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            Parser = typeParser ?? throw new ArgumentNullException(nameof(typeParser));
        }

        public ITypeFilter Filter { get; }

        public ITypeParser Parser { get; }

        public ITypeResolver Resolver { get; }

        public List<Type> Visited { get; } = new List<Type>();
        IEnumerable<RuntimeType> RegisteredTypes => Types.Values;
        IDictionary<string, RuntimeType> Types { get; } = new Dictionary<string, RuntimeType>();

        RuntimeType this[string id, RuntimeType type]
        {
            get
            {
                if (!Types.ContainsKey(id))
                    Types.Add(id, type);
                Types[id].RegisterType(type.Type);
                return Types[id];
            }
        }

        public bool CanCreate(Type type) => Filter.CanCreate(type);

        public bool CanRegister(Type type) => Filter.CanRegister(type);

        public object CreateInstance(Type type, params object[] args) => CreateInstance(type.FullName, args);

        public object CreateInstance(string id, params object[] args)
        {
            if (Types.ContainsKey(id))
                return Types[id].CreateInstance(args);
            var parameterArgs = GetParameterArgs(args);
            var runtimeType = (RuntimeType)Parser.Find(id, parameterArgs, Types.Values);
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
            string id = Resolver.GetTypeId(attribute, type.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
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
                RegisterConstructorType(constructorInfo, constructor);
            }
        }

        void RegisterConstructorParameter(int i, Type type, RuntimeType constructor, ParameterInfo[] parameters)
        {
            var parameterType = parameters[i].ParameterType;
            var injectionAttribute = GetParameterAttribute(parameters[i]);
            var parameter = new RuntimeType(injectionAttribute, constructor, parameters[i].ParameterType, injectionAttribute.Args);
            string id = Resolver.GetTypeId(injectionAttribute, parameterType.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
            var parameterArgs = GetInjectedParameterArgs(type, parameterType, injectionAttribute, id, attributeType);
            var runtimeType = Parser.Find(id, parameterArgs, Types.Values);
            if (runtimeType == null)
                RegisterConstructorType(attributeType);
            RegisterConstructorType(parameterType);
            string typeFullName = Resolver.GetTypeFullName(runtimeType, id, parameterArgs);
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                result.Attribute.RegisterRuntimeType(string.Format("{0}:({1})", GetParameterTypeFullName(type, parameters), i), injectionAttribute);
                constructor.AddParameter(result);
            }
        }

        void RegisterConstructorType(Type type)
        {
            if (Filter.CanRegister(type))
            {
                if (Visited.Contains(type))
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

        void RegisterConstructorType(ConstructorInfo constructorInfo, RuntimeType constructor)
        {
            var attribute = constructor.Attribute;
            string id = GetTypeId(constructorInfo.DeclaringType, attribute);
            var parameterArgs = constructorInfo.GetParameters().Select(p => p.ParameterType.FullName).ToArray();
            var runtimeType = Parser.Find(id, parameterArgs, Types.Values);
            string typeFullName = Resolver.GetTypeFullName(runtimeType, id, parameterArgs);
            if (!Types.ContainsKey(typeFullName) || !this[typeFullName, Types[typeFullName]].IsInitialized)
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.Initialize(attribute.RuntimeInstance);
                }
            }
        }

        void RegisterTypeId(Type type)
        {
            Visited.Add(type);
            RegisterConstructor(type);
            Visited.Remove(type);
        }
    }
}