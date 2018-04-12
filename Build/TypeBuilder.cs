using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Build
{
    class TypeBuilder
    {
        ITypeResolver _typeResolver;
        public TypeBuilder(ITypeResolver typeResolver) => _typeResolver = typeResolver;
        IDictionary<string, RuntimeType> types = new Dictionary<string, RuntimeType>();
        RuntimeType this[string type]
        {
            get
            {
                if (!types.ContainsKey(type)) types.Add(type, new RuntimeType(type));
                return types[type];
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
                if (types.ContainsKey(id)) return types[id].CreateInstance();
                throw new Exception(string.Format("{0} is not instantiable (no constructors available)", typeName));
            }
            return CreateInstance(GetTypeId(type.GetCustomAttribute<DependencyAttribute>(), _typeResolver.GetName(type)), type.FullName);
        }
        public void RegisterType(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
                throw new Exception(string.Format("{0} is not registered (no constructors available)", type.FullName));
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters().ToList();
                var args = new List<RuntimeType>();
                var runtimeInstance = RuntimeInstance.CreateInstance;
                {
                    var attribute = constructor.GetCustomAttribute<DependencyAttribute>();
                    if (attribute == null)
                        attribute = type.GetCustomAttribute<DependencyAttribute>();
                    string typeId = GetTypeId(attribute, _typeResolver.GetName(type));
                    var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                    if (attributeType != null && !attributeType.IsAssignableFrom(type))
                        throw new Exception(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
                    if (attribute != null && attribute.Runtime != runtimeInstance)
                        runtimeInstance = attribute.Runtime;
                    object init()
                    {
                        Debug.WriteLine("{0}({1})", type.FullName, string.Join(",", args.Select(p => p.Id)));
                        return Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                    }
                    this[typeId].RegiterType(runtimeInstance, type.FullName, type, init);
                }
                foreach (var parameterInfo in parameters)
                {
                    var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    var parameterType = parameterInfo.ParameterType;
                    string typeId = GetTypeId(attribute, _typeResolver.GetName(parameterType));
                    var attributeType = _typeResolver.GetType(type.Assembly, typeId);
                    if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                        throw new Exception(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                    if (typeId == _typeResolver.GetName(type) && typeId == _typeResolver.GetName(parameterType))
                        throw new Exception(string.Format("{0} is not registered (circular references found)", type.FullName));
                    args.Add(this[typeId]);
                }
            }
        }
    }
}
