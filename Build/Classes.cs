using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Build
{
    class RuntimeType
    {
        bool _init;
        bool _guard;
        object _instance;
        Func<object> _func;
        RuntimeInstance _runtime;
        Type _type;
        string _id;
        public string Id { get { return _id; } }
        public RuntimeType(string id) => _id = id;
        public void RegiterType(RuntimeInstance runtime, string id, Type type, Func<object> func)
        {
            if (_init)
                throw new Exception(string.Format("{0} is not registered (more than one constructors available)", Id));
            _runtime = runtime;
            _type = type;
            _id = _type.FullName;
            _func = func;
            _init = true;
        }
        public object CreateInstance()
        {
            object Evaluate()
            {
                if (_guard)
                    throw new Exception(string.Format("{0} is not evaluated (circular references found)", Id));
                _guard = true;
                object result = _func();
                _guard = false;
                return result;
            }
            switch (_runtime)
            {
                case RuntimeInstance.Singleton:
                    if (!_guard)
                    {
                        _instance = Evaluate();
                        _guard = true;
                    }
                    return _instance;
                case RuntimeInstance.CreateInstance:
                    return Evaluate();
                case RuntimeInstance.None:
                default:
                    if (_func != null)
                        throw new Exception(string.Format("{0} is not evaluated (no instance)", Id));
                    return _instance;
            }
        }
    }
    class TypeBuilder
    {
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
                if (attribute.Type != null) return attribute.Type.FullName;
                if (attribute.Id != null) return attribute.Id;
            }
            return defaultValue;
        }
        public object CreateInstance(Type type)
        {
            object CreateInstance(string id)
            {
                if (types.ContainsKey(id)) return types[id].CreateInstance();
                throw new Exception(string.Format("{0} is not instantiable (no constructors available)", id));
            }
            return CreateInstance(GetTypeId(type.GetCustomAttribute<DependencyAttribute>(), type.FullName));
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
                RuntimeInstance runtimeInstance = RuntimeInstance.CreateInstance;
                {
                    var attribute = constructor.GetCustomAttribute<DependencyAttribute>();
                    if (attribute == null)
                        attribute = type.GetCustomAttribute<DependencyAttribute>();
                    string typeId = GetTypeId(attribute, type.FullName);
                    var attributeType = type.Assembly.GetType(typeId);
                    if (attributeType != null && !attributeType.IsAssignableFrom(type))
                        throw new Exception(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, type.FullName));
                    if (attribute != null && attribute.Runtime != runtimeInstance)
                        runtimeInstance = attribute.Runtime;
                    object init()
                    {
                        Debug.WriteLine("{0}({1})", type.FullName, string.Join(",", args.Select(p => p.Id)));
                        return Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                    }
                    this[typeId].RegiterType(runtimeInstance, typeId, type, init);
                }
                foreach (var parameterInfo in parameters)
                {
                    var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    var parameterType = parameterInfo.ParameterType;
                    string typeId = GetTypeId(attribute, parameterType.FullName);
                    var attributeType = type.Assembly.GetType(typeId);
                    if (attributeType != null && !parameterType.IsAssignableFrom(attributeType))
                        throw new Exception(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
                    if (typeId == type.FullName && typeId == parameterType.FullName)
                        throw new Exception(string.Format("{0} is not registered (circular references found)", type.FullName, parameterInfo.Name));
                    args.Add(this[typeId]);
                }
            }
        }
    }
}
