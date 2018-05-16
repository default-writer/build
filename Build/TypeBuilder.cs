using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

namespace Build
{
    class TypeBuilder
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

        ITypeFilter Filter { get; }

        ITypeParser Parser { get; }

        ITypeResolver Resolver { get; }

        IDictionary<string, RuntimeType> Types { get; } = new Dictionary<string, RuntimeType>();

        List<Type> Visited { get; } = new List<Type>();

        RuntimeType this[string id, RuntimeType type]
        {
            get
            {
                if (!Types.ContainsKey(id))
                    Types.Add(id, type);
                Types[id].RegisterAssignableType(type.Type);
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
            var parameterArgs = GetParametersFullName(args);
            var runtimeType = (RuntimeType)Parser.Find(id, parameterArgs, Types.Values);
            if (runtimeType != null) return runtimeType.CreateInstance(args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", id));
        }

        public void RegisterType(Type type)
        {
            Visited.Add(type);
            RegisterConstructor(type);
            Visited.Remove(type);
        }

        internal void Reset() => Types.Clear();

        static void CheckParameterTypeFullName(Type type, Type parameterType, string id)
        {
            if (id == type.FullName && id == parameterType.FullName)
                throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
        }

        static DependencyAttribute GetDependencyAttribute(ConstructorInfo constructor) => constructor.GetCustomAttribute<DependencyAttribute>() ?? new DependencyAttribute(constructor.DeclaringType, RuntimeInstance.CreateInstance);

        static InjectionAttribute GetInjectionAttribute(ParameterInfo parameter) => parameter.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameter.ParameterType);

        static string[] GetParametersFullName(Type type, Type parameterType, InjectionAttribute injectionAttribute, string id, Type attributeType)
        {
            if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
            CheckParameterTypeFullName(type, parameterType, id);
            return GetParametersFullName(injectionAttribute.Args);
        }

        static string[] GetParametersFullName(object[] args) => args == null ? Array.Empty<string>() : args.Select(p => (p ?? typeof(object)).GetType().FullName).ToArray();

        string GetId(Type type, IRuntimeAttribute attribute)
        {
            string id = Resolver.GetTypeFullName(attribute, type.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
            if (attributeType != null && !attributeType.IsAssignableFrom(type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
            return id;
        }

        private string GetTypeFullName(Type type, ParameterInfo[] parameters, IRuntimeAttribute attribute)
        {
            string id = GetId(type, attribute);
            var parameterArgs = parameters.Select(p => p.ParameterType.FullName).ToArray();
            var runtimeType = Parser.Find(id, parameterArgs, Types.Values);
            string constructorRuntimeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            string typeFullName = Format.GetConstructorFullName(constructorRuntimeFullName, parameterArgs);
            return typeFullName;
        }

        void RegisterConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructorInfo in constructors)
            {
                var parameters = constructorInfo.GetParameters();
                var dependencyAttribute = GetDependencyAttribute(constructorInfo);
                var constructor = new RuntimeType(dependencyAttribute, null, type);
                for (int i = 0; i < parameters.Length; i++)
                {
                    RegisterConstructorParameter(i, type, constructor, parameters);
                }
                var attribute = constructor.Attribute;
                string typeFullName = GetTypeFullName(type, parameters, attribute);
                RegisterConstructorType(typeFullName, constructor, attribute);
            }
        }

        void RegisterConstructorParameter(int i, Type type, RuntimeType constructor, ParameterInfo[] parameterArray)
        {
            var parameterType = parameterArray[i].ParameterType;
            var injectionAttribute = GetInjectionAttribute(parameterArray[i]);
            var parameter = new RuntimeType(injectionAttribute, constructor, parameterArray[i].ParameterType);
            string id = Resolver.GetTypeFullName(injectionAttribute, parameterType.FullName);
            var attributeType = Resolver.GetType(type.Assembly, id);
            var parameters = GetParametersFullName(type, parameterType, injectionAttribute, id, attributeType);
            var runtimeType = Parser.Find(id, parameters, Types.Values);
            if (runtimeType == null)
                RegisterConstructorType(attributeType);
            RegisterConstructorType(parameterType);
            string constructorRuntimeTypeFullName = runtimeType == null ? id : runtimeType.Type.FullName;
            string typeFullName = Format.GetConstructorFullName(constructorRuntimeTypeFullName, parameters);
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                var constructorFullName = Format.GetConstructorFullName(type.FullName, parameterArray.Select(p => p.ParameterType.FullName));
                var parameterId = Format.GetConstructorParameterFullName(constructorFullName, i);
                result.Attribute.RegisterRuntimeType(parameterId, injectionAttribute);
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
                    RegisterType(type);
                }
                catch (TypeRegistrationException ex)
                {
                    throw ex;
                }
            }
        }

        void RegisterConstructorType(string typeFullName, RuntimeType constructor, IRuntimeAttribute runtimeAttribute)
        {
            if (!Types.ContainsKey(typeFullName) || !this[typeFullName, Types[typeFullName]].IsInitialized)
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.Initialize(runtimeAttribute.RuntimeInstance);
                }
            }
        }
    }
}