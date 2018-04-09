using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApp1
{


    public enum Runtime
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
        public Type Type { get; private set; }
        public string Id { get; private set; }
        public Runtime Runtime { get; private set; } = Runtime.CreateInstance;
        public DependencyAttribute(string id, Runtime runtime) { Id = id; Runtime = runtime; }
        public DependencyAttribute(Type type, Runtime runtime) { Type = type; Runtime = runtime; }
        public DependencyAttribute(Runtime runtime) { Runtime = runtime; }
        public DependencyAttribute(string id) { Id = id; }
        public DependencyAttribute(Type type) { Type = type; }
        public DependencyAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute, IRuntimeAttribute
    {
        public Type Type { get; private set; }
        public string Id { get; private set; }
        public InjectAttribute(string id) { Id = id; }
        public InjectAttribute(Type type) { Type = type; }
    }

    class RuntimeType
    {
        private volatile bool _initialized;

        private volatile object _instance;

        private Func<object> _func;

        private Runtime _type;

        private string _id;

        public RuntimeType(string id) { _id = id; }

        public bool Initialized
        {
            get
            {
                return _initialized;
            }
        }

        public Runtime Type
        {
            get
            {
                return _type;
            }
        }
        public string Id
        {
            get
            {
                return _id;
            }
        }

        public void Initialize(Func<object> func, Runtime type, string id)
        {
            if (_id != id)
            {
                throw new Exception(string.Format("{0} is amibiguous for initialization", _id));
            }
            if (_initialized)
            {
                throw new Exception(string.Format("{0} is already initialized", _id));
            }
            lock (this)
            {
                if (!_initialized)
                {
                    _func = func ?? throw new Exception(string.Format("{0} is required iniitialization", _id));
                    _type = type;
                    _initialized = true;
                }
            }
        }

        internal object CreateInstance()
        {
            switch (_type)
            {
                case Runtime.Singleton:
                    if (_instance == null)
                    {
                        lock (this)
                        {
                            if (_instance == null)
                            {
                                _instance = _func();
                            }
                        }
                    }
                    return _instance;
                case Runtime.CreateInstance:
                    if (_func != null)
                    {
                        return _func();
                    }
                    return _instance;
                case Runtime.None:
                default:
                    if (_func != null)
                    {
                        throw new Exception(string.Format("{0} is not allowed to initialize", _id));
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

    class TypeBuilder
    {
        private IDictionary<string, RuntimeType> types = new Dictionary<string, RuntimeType>();

        public object CreateInstance(Type type)
        {
            string id = type.FullName;
            DependencyAttribute classAttribute = type.GetCustomAttribute<DependencyAttribute>();
            if (classAttribute != null)
            {
                if (classAttribute.Id != null)
                {
                    id = classAttribute.Id;
                }
            }
            if (types.ContainsKey(id))
            {
                return types[id].CreateInstance();
            }
            return default;
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
                        if (!referenceType.IsAssignableFrom(instanceType))
                        {
                            throw new Exception(string.Format("{0} is not assignable from to {1}", referenceType.FullName, instanceType.FullName));
                        }
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
            void LoadConstructor(ConstructorInfo mi, List<RuntimeType> args)
            {
                Runtime runtimeInstance = Runtime.CreateInstance;
                string typeId = type.FullName;
                DependencyAttribute attribute = mi.GetCustomAttribute<DependencyAttribute>();
                if (attribute == null)
                {
                    attribute = type.GetCustomAttribute<DependencyAttribute>();
                }
                typeId = GetName(attribute, type, typeId);
                if (attribute != null)
                {
                    if (attribute.Runtime != runtimeInstance)
                    {
                        runtimeInstance = attribute.Runtime;
                    }
                }
                if (types.ContainsKey(typeId))
                {
                    if (types[typeId].Initialized)
                    {
                        throw new Exception(string.Format("{0} is amibiguous for creation", typeId));
                    }
                }
                object init()
                {
                    Console.WriteLine("{0}({1})", type.FullName, string.Join(",", args.Select(p => p.Id)));
                    return Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                }
                this[typeId].Initialize(init, runtimeInstance, typeId);
            }
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                List<ParameterInfo> parameters = constructor.GetParameters().ToList();
                List<RuntimeType> args = new List<RuntimeType>();
                IEnumerator<ParameterInfo> parametersEnumerator = parameters.GetEnumerator();
                while (parametersEnumerator.MoveNext())
                {
                    ParameterInfo parameterInfo = parametersEnumerator.Current;
                    InjectAttribute attribute = parameterInfo.GetCustomAttribute<InjectAttribute>();
                    Type parameterType = parameterInfo.ParameterType;
                    string typeId = type.FullName;
                    typeId = GetName(attribute, parameterType, parameterType.FullName);
                    if (type.FullName == typeId)
                    {
                        typeId = parameterType.FullName;
                    }
                    args.Add(this[typeId]);
                }
                LoadConstructor(constructor, args);
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
        Func<Type, bool> _filter = type => type.IsPublic &&
            !type.IsInterface &&
            !type.IsAbstract &&
            !type.IsValueType &&
            !type.IsGenericType &&
            !typeof(Attribute).IsAssignableFrom(type) &&
            !typeof(MarshalByRefObject).IsAssignableFrom(type);

        private TypeBuilder typeBuilder = new TypeBuilder();

        public T CreateInstance<T>()
        {
            if (!_filter(typeof(T)))
            {
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            }
            return (T)typeBuilder.CreateInstance(typeof(T));
        }

        public void RegisterType<T>()
        {
            if (!_filter(typeof(T)))
            {
                throw new Exception(string.Format("{0} is not allowed", typeof(T).FullName));
            }
            typeBuilder.RegisterType(typeof(T));
        }

        public void RegisterAssemblyTypes(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (_filter(type))
                {
                    typeBuilder.RegisterType(type);
                }
            }
        }
    }
}
