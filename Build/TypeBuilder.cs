using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Build
{
    class TypeBuilder
    {
        IDictionary<string, RuntimeType> types = new Dictionary<string, RuntimeType>();
        ITypeFilter _typeFilter;
        ITypeResolver _typeResolver;
        public ITypeFilter Filter => _typeFilter;
        public ITypeResolver Resolver => _typeResolver;
        public TypeBuilder()
        {
            _typeFilter = new TypeFilter();
            _typeResolver = new TypeResolver();
        }
        public TypeBuilder(ITypeFilter typeFilter, ITypeResolver typeResolver)
        {
            _typeFilter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            _typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
        }
        RuntimeType this[string typeId, Type type]
        {
            get
            {
                if (!types.ContainsKey(typeId))
                    types.Add(typeId, new RuntimeType(type));
                return types[typeId];
            }
        }
        string GetTypeId(IRuntimeAttribute attribute, string defaultValue)
        {
            if (attribute != null)
            {
                if (attribute.Type != null) return _typeResolver.GetName(attribute.Type);
                if (attribute.Id != null) return attribute.Id;
            }
            return defaultValue;
        }
        public object CreateInstance(Type type)
        {
            object CreateInstance(string id, string typeName)
            {
                //var constructors = types.Select(p => p.Value).TakeWhile(p => p.Type == type).ToArray();
                //if (constructors.Length == 1)
                //    return constructors[0].CreateInstance();
                //if (constructors.Length == 0)
                //if (types.ContainsKey(id))
                //    return types[id].CreateInstance();
                //foreach (var constructor in type.GetConstructors())
                //{
                //    var attribute = constructor.GetCustomAttribute<ConstructorAttribute>();
                //    if (attribute == null)
                //    {
                //        attribute = new ConstructorAttribute(string.Format("{0}({1})", type.FullName, string.Join(", ", constructor.GetParameters().Select(p => p.ParameterType.FullName).ToArray())));
                //    }
                //    //    attribute = type.GetCustomAttribute<DependencyAttribute>();
                //    string typeId = GetTypeId(attribute, _typeResolver.GetName(type));
                //    if (typeId == id && types.ContainsKey(typeId))
                //        return types[typeId].CreateInstance();
                //}
                //var constructors = types.Select(p => p.Value).TakeWhile(p => p.Type == type).ToArray();
                //if (constructors.Length == 1)
                //    return constructors[0].CreateInstance();
                if (types.ContainsKey(id)) return types[id].CreateInstance();
                throw new Exception(string.Format("{0} is not instantiable (no constructors available)", typeName));
            }
            return CreateInstance(/*GetTypeId(type.GetCustomAttribute<DependencyAttribute>(), */_typeResolver.GetName(type)/*)*/, type.FullName);
        }
        HashSet<Type> visited = new HashSet<Type>();
        void RegisterConstructorParameters(Type type)
        {
            if (visited.Contains(type))
                throw new Exception(string.Format("{0} is not registered (circular references found)", type.FullName));
            if (!IsRegistered(type, null))
            {
                visited.Add(type);
                RegisterConstructor(type);
                visited.Remove(type);
            }
        }
        public void RegisterType(Type type)
        {
            visited.Clear();
            visited.Add(type);
            RegisterConstructor(type);
        }
        bool IsRegistered(Type type, IRuntimeAttribute attribute)
        {
            string typeId = GetTypeId(attribute, _typeResolver.GetName(type));
            return types.ContainsKey(typeId) && this[typeId, type].IsRegistered;
        }
        void RegisterConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new Exception(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructor in constructors)
            {
                var args = new List<RuntimeType>();
                var runtimeInstance = RuntimeInstance.CreateInstance;
                {
                    var attribute = constructor.GetCustomAttribute<DependencyAttribute>();
                    string typeId = GetTypeId(attribute, _typeResolver.GetName(type));
                    if (attribute != null && IsRegistered(type, attribute))
                        continue;
                    var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                    if (attributeType != null && !attributeType.IsAssignableFrom(type))
                        throw new TypeDependencyException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
                    if (attribute != null)
                        runtimeInstance = attribute.Runtime;
                    object init() => Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                    Debug.WriteLine("{0}({1})", type.FullName, string.Join(", ", args.Select(p => p.Id)));
                    this[typeId, type].RegisterType(runtimeInstance, type.FullName, type, init);
                }
                foreach (var parameterInfo in constructor.GetParameters())
                {
                    var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    var parameterType = parameterInfo.ParameterType;
                    string typeId = GetTypeId(attribute, _typeResolver.GetName(parameterType));
                    var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                    if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                        throw new TypeInjectionException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                    if (typeId == _typeResolver.GetName(type) && typeId == _typeResolver.GetName(parameterType))
                        throw new TypeInjectionException(string.Format("{0} is not registered (circular references found)", type.FullName));
                    args.Add(this[typeId, parameterType]);
                    if (_typeFilter.CanRegister(parameterType))
                        RegisterConstructorParameters(parameterType);
                }
            }
        }
    }
}
