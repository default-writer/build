using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Build
{
    public enum RuntimeInstance
    {
        None,
        CreateInstance,
        Singleton
    }

    public interface IRuntimeAttribute
    {
        Type Type { get; }
        string Id { get; }
    }

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Class)]
    public class DependencyAttribute : Attribute, IRuntimeAttribute
    {
        public Type Type { get; }
        public string Id { get; }
        public RuntimeInstance Runtime { get; } = RuntimeInstance.CreateInstance;
        public DependencyAttribute(string id, RuntimeInstance runtime) { Id = id; Runtime = runtime; }
        public DependencyAttribute(Type type, RuntimeInstance runtime) { Type = type; Runtime = runtime; }
        public DependencyAttribute(RuntimeInstance runtime) { Runtime = runtime; }
        public DependencyAttribute(string id) { Id = id; }
        public DependencyAttribute(Type type) { Type = type; }
        public DependencyAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectionAttribute : Attribute, IRuntimeAttribute
    {
        public Type Type { get; }
        public string Id { get; }
        public InjectionAttribute(string id) { Id = id; }
        public InjectionAttribute(Type type) { Type = type; }
    }

    internal class RuntimeType
    {
        bool _init;
        object _instance;
        bool _reference;
        Func<object> _func;
        RuntimeInstance _runtime;
        Type _type;
        string _id;

        public RuntimeType(string id) { _id = id; }

        public Type Type { get { return _type; } }
        public string Id { get { return _id; } }

        public void Initialize(RuntimeInstance runtime, string id, Type type, Func<object> func)
        {
            if (id == null)
            {
                throw new Exception(string.Format("{0} parameter is required", nameof(id)));
            }
            if (type == null)
            {
                throw new Exception(string.Format("{0} parameter is required", nameof(type)));
            }
            if (func == null)
            {
                throw new Exception(string.Format("{0} parameter is required", nameof(func)));
            }
            _type = type;
            if (_init)
            {
                throw new Exception(string.Format("{0} is amibiguous for default initialization (more than one matching constructor found)", Id));
            }
            if (_id != id)
            {
                throw new Exception(string.Format("{0} is amibiguous for initialization", Id));
            }
            _id = _type.FullName;
            lock (this)
            {
                if (!_init)
                {
                    _func = func;
                    _runtime = runtime;
                    _init = true;
                }
            }
        }

        public object CreateInstance()
        {
            object Evaluate()
            {
                if (!_init)
                {
                    throw new Exception(string.Format("{0} is required to initialize", Id));
                }
                if (_func == null)
                {
                    throw new Exception(string.Format("{0} is required to evaluate", Id));
                }
                if (_reference)
                {
                    throw new Exception(string.Format("{0} is referenced more than once", Id));
                }
                lock (this)
                {
                    object result;
                    _reference = true;
                    result = _func();
                    _reference = false;
                    return result;
                }
            }
            switch (_runtime)
            {
                case RuntimeInstance.Singleton:
                    if (_instance == null)
                    {
                        lock (this)
                        {
                            if (_instance == null)
                            {
                                _instance = Evaluate();
                            }
                        }
                    }
                    return _instance;
                case RuntimeInstance.CreateInstance:
                    if (_func != null)
                    {
                        return Evaluate();
                    }
                    return _instance;
                case RuntimeInstance.None:
                default:
                    if (_func != null)
                    {
                        throw new Exception(string.Format("{0} is not allowed to initialize", Id));
                        //_instance = _func();
                    }
                    return _instance;
            }
        }
    }

    public interface IContainer
    {
        T CreateInstance<T>();
        void RegisterType<T>();
    }

    internal class TypeBuilder
    {
        IDictionary<string, RuntimeType> types = new Dictionary<string, RuntimeType>();

        public object CreateInstance(Type type)
        {
            object CreateInstance(string id)
            {
                if (types.ContainsKey(id))
                {
                    return types[id].CreateInstance();
                }
                return default;
            }
            DependencyAttribute classAttribute = type.GetCustomAttribute<DependencyAttribute>();
            if (classAttribute != null)
            {
                if (classAttribute.Id != null)
                {
                    return CreateInstance(classAttribute.Id);
                }
                if (classAttribute.Type != null)
                {
                    return CreateInstance(classAttribute.Type.FullName);
                }
            }
            return CreateInstance(type.FullName);
        }

        public void RegisterType(Type type)
        {
            string GetName(IRuntimeAttribute attribute, Type referenceType, string defaultValue)
            {
                if (attribute != null)
                {
                    Type instanceType = attribute.Type;
                    if (instanceType != null)
                    {
                        return attribute.Type.FullName;
                    }
                    else
                    {
                        if (attribute.Id != null)
                        {
                            return attribute.Id;
                        }
                        return defaultValue;
                    }
                }
                else
                {
                    return defaultValue;
                }
            }
            void LoadConstructor(RuntimeInstance runtimeInstance, ConstructorInfo mi, List<RuntimeType> args)
            {
                string typeId = type.FullName;
                DependencyAttribute attribute = mi.GetCustomAttribute<DependencyAttribute>();
                if (attribute == null)
                {
                    attribute = type.GetCustomAttribute<DependencyAttribute>();
                }
                if (attribute != null && attribute.Type != null && !attribute.Type.IsAssignableFrom(type))
                {
                    throw new Exception(string.Format("{0} is not assignable from to {1}", attribute.Type.FullName, type.FullName));
                }
                typeId = GetName(attribute, type, typeId);
                if (attribute != null)
                {
                    if (attribute.Runtime != runtimeInstance)
                    {
                        runtimeInstance = attribute.Runtime;
                    }
                }
                object init()
                {
                    Debug.WriteLine("{0}({1})", type.FullName, string.Join(",", args.Select(p => p.Id)));
                    return Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                }
                this[typeId].Initialize(runtimeInstance, typeId, type, init);
            }
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                List<ParameterInfo> parameters = constructor.GetParameters().ToList();
                List<RuntimeType> args = new List<RuntimeType>();
                IEnumerator<ParameterInfo> parametersEnumerator = parameters.GetEnumerator();
                while (parametersEnumerator.MoveNext())
                {
                    ParameterInfo parameterInfo = parametersEnumerator.Current;
                    InjectionAttribute attribute = parameterInfo.GetCustomAttribute<InjectionAttribute>();
                    Type parameterType = parameterInfo.ParameterType;
                    if (attribute != null && attribute.Type != null && !parameterType.IsAssignableFrom(attribute.Type))
                    {
                        throw new Exception(string.Format("{0} is not assignable from to {1}", parameterType.FullName, attribute.Type.FullName));
                    }
                    string typeId = type.FullName;
                    typeId = GetName(attribute, parameterType, parameterType.FullName);
                    if (type.FullName == typeId)
                    {
                        typeId = parameterType.FullName;
                    }
                    args.Add(this[typeId]);
                }
                LoadConstructor(RuntimeInstance.CreateInstance, constructor, args);
            }
        }

        RuntimeType this[string type]
        {
            get
            {
                if (!types.ContainsKey(type))
                {
                    types.Add(type, new RuntimeType(type));
                }
                return types[type];
            }
        }
    }

    public class Container : IContainer
    {
        bool _registerFilter(Type type) => 
            !type.IsInterface &&
            !type.IsAbstract &&
            !type.IsValueType &&
            !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) &&
            !typeof(MarshalByRefObject).IsAssignableFrom(type) &&
            _createFilter(type);

        bool _createFilter(Type type) => type.IsPublic;

        TypeBuilder typeBuilder = new TypeBuilder();

        public T CreateInstance<T>()
        {
            if (!_createFilter(typeof(T)))
            {
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            }
            return (T)typeBuilder.CreateInstance(typeof(T));
        }

        public void RegisterType<T>()
        {
            if (!_registerFilter(typeof(T)))
            {
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            }
            typeBuilder.RegisterType(typeof(T));
        }

        public void RegisterAssemblyTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new Exception(nameof(assembly));
            }

            foreach (Type type in assembly.GetTypes())
            {
                if (_registerFilter(type))
                {
                    typeBuilder.RegisterType(type);
                }
            }
        }
    }
}
