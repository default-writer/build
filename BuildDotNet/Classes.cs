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
        public void Initialize(RuntimeInstance runtime, string id, Type type, Func<object> func)
        {
            if (_init)
                throw new Exception(string.Format("{0} is amibiguous for initialization (more than one matching initialization method found)", Id));
            if (Id != id)
                throw new Exception(string.Format("{0} is amibiguous for initialization", Id));
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
                if (!_init)
                    throw new Exception(string.Format("{0} is required to initialize", Id));
                if (_func == null)
                    throw new Exception(string.Format("{0} is required to evaluate", Id));
                if (_guard)
                    throw new Exception(string.Format("{0} is already evaluated", Id));
                _guard = true;
                object result = _func();
                _guard = false;
                return result;
            }
            switch (_runtime)
            {
                case RuntimeInstance.Singleton:
                    if (_instance == null)
                        _instance = Evaluate();
                    return _instance;
                case RuntimeInstance.CreateInstance:
                    if (_func != null)
                        return Evaluate();
                    return _instance;
                case RuntimeInstance.None:
                default:
                    if (_func != null) throw new Exception(string.Format("{0} is not allowed to initialize", Id));
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
                if (!types.ContainsKey(type))
                    types.Add(type, new RuntimeType(type));
                return types[type];
            }
        }
        public object CreateInstance(Type type)
        {
            object CreateInstance(string id)
            {
                if (types.ContainsKey(id))
                    return types[id].CreateInstance();
                return default;
            }
            var classAttribute = type.GetCustomAttribute<DependencyAttribute>();
            if (classAttribute != null)
            {
                if (classAttribute.Id != null)
                    return CreateInstance(classAttribute.Id);
                if (classAttribute.Type != null)
                    return CreateInstance(classAttribute.Type.FullName);
            }
            return CreateInstance(type.FullName);
        }
        public void RegisterType(Type type)
        {
            string GetTypeId(IRuntimeAttribute attribute, Type referenceType, string defaultValue)
            {
                if (attribute != null)
                {
                    var instanceType = attribute.Type;
                    if (instanceType != null)
                        return attribute.Type.FullName;
                    else
                    {
                        if (attribute.Id != null)
                            return attribute.Id;
                        return defaultValue;
                    }
                }
                else
                    return defaultValue;
            }
            void Initialize(RuntimeInstance runtimeInstance, ConstructorInfo mi, List<RuntimeType> args)
            {
                string typeId = type.FullName;
                var attribute = mi.GetCustomAttribute<DependencyAttribute>();
                if (attribute == null)
                    attribute = type.GetCustomAttribute<DependencyAttribute>();
                if (attribute != null && attribute.Type != null && !attribute.Type.IsAssignableFrom(type))
                    throw new Exception(string.Format("{0} is not assignable from {1}", attribute.Type.FullName, type.FullName));
                typeId = GetTypeId(attribute, type, typeId);
                if (attribute != null && attribute.Runtime != runtimeInstance)
                    runtimeInstance = attribute.Runtime;
                object init()
                {
                    Debug.WriteLine("{0}({1})", type.FullName, string.Join(",", args.Select(p => p.Id)));
                    return Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                }
                this[typeId].Initialize(runtimeInstance, typeId, type, init);
            }
            foreach (var constructor in type.GetConstructors())
            {
                var parameters = constructor.GetParameters().ToList();
                var args = new List<RuntimeType>();
                foreach (var parameterInfo in parameters)
                {
                    var attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    var parameterType = parameterInfo.ParameterType;
                    if (attribute != null && attribute.Type != null && !parameterType.IsAssignableFrom(attribute.Type))
                        throw new Exception(string.Format("{0} is not assignable from {1}", parameterType.FullName, attribute.Type.FullName));
                    string typeId = type.FullName;
                    typeId = GetTypeId(attribute, parameterType, parameterType.FullName);
                    if (type.FullName == typeId)
                        typeId = parameterType.FullName;
                    args.Add(this[typeId]);
                }
                Initialize(RuntimeInstance.CreateInstance, constructor, args);
            }
        }
    }
    public interface IContainer
    {
        T CreateInstance<T>();
        void RegisterType<T>();
    }
    public class Container : IContainer
    {
        bool _createFilter(Type type) => type.IsPublic;
        bool _registerFilter(Type type) =>
            !type.IsInterface && !type.IsAbstract && !type.IsValueType && !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) && !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);

        TypeBuilder typeBuilder = new TypeBuilder();
        public T CreateInstance<T>()
        {
            if (!_createFilter(typeof(T)))
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            return (T)typeBuilder.CreateInstance(typeof(T));
        }
        public void RegisterType<T>()
        {
            if (!_registerFilter(typeof(T)))
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            typeBuilder.RegisterType(typeof(T));
        }
        public void RegisterAssemblyTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new Exception(nameof(assembly));
            foreach (var type in assembly.GetTypes())
                if (_registerFilter(type))
                    typeBuilder.RegisterType(type);
        }
    }
}
