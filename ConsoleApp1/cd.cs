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
        public DependencyAttribute(Runtime runtime) { Runtime = runtime; }
        public DependencyAttribute(string id) { Id = id; }
        public DependencyAttribute(Type type) { Type = type; }
        public DependencyAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
        public Type Type { get; private set; }
        public string Id { get; private set; }
        public InjectAttribute(string serviceName) { Id = serviceName; }
        public InjectAttribute(Type type) { Type = type; }
    }

    class RuntimeType
    {
        private volatile object _instance;

        private Func<object> _func;

        private Runtime _type;

        private string _id;

        //public Func<object> Func
        //{
        //    get
        //    {
        //        return _func;
        //    }
        //    set
        //    {
        //        if (value != _func)
        //        {
        //            _instance = default;
        //        }
        //        _func = value;
        //    }
        //}

        //public Runtime Type
        //{
        //    get
        //    {
        //        return _type;
        //    }
        //    set
        //    {
        //        if (_type != value)
        //        {
        //            _instance = default;
        //        }
        //        _type = value;
        //    }
        //}

        public void Initialize(Func<object> func, Runtime type, string id)
        {
            if (_func != null)
            {
                throw new Exception(string.Format("{0} type already initialized", id));
            }
            _func = func;
            _type = type;
            _id = id;
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
                case Runtime.None:
                default:
                    if (_func != null)
                    {
                        return _func();
                    }
                    return _instance;
            }
        }
    }
    interface IContainer
    {
        T CreateInstance<T>();
        void RegisterType<T>();
    }

    class TypeBuilder
    {
        private IDictionary<string, RuntimeType> d = new Dictionary<string, RuntimeType>();

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
            if (d.ContainsKey(id))
            {
                return d[id].CreateInstance();
            }
            return default;
        }

        public void RegisterType(Type type)
        {
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                List<ParameterInfo> parameters = constructor.GetParameters().ToList();
                List<RuntimeType> args = new List<RuntimeType>();
                IEnumerator<ParameterInfo> parametersEnumerator = parameters.GetEnumerator();
                while (parametersEnumerator.MoveNext())
                {
                    ParameterInfo parameterInfo = parametersEnumerator.Current;
                    string parameterName = default;
                    Type dependencyType = parameterInfo.ParameterType;
                    InjectAttribute attribute = parameterInfo.GetCustomAttribute<InjectAttribute>();
                    if (attribute != null)
                    {
                        Type instanceType = attribute.Type;
                        if (instanceType != null)
                        {
                            if (!dependencyType.IsAssignableFrom(instanceType))
                            {
                                throw new Exception(string.Format("{0} is not assignable from to {1}", dependencyType.FullName, instanceType.FullName));
                            }
                            parameterName = attribute.Type.FullName;
                        }
                        else
                        {
                            if (attribute.Id != null)
                            {
                                parameterName = attribute.Id;
                            }
                        }
                    }
                    else
                    {
                        parameterName = dependencyType.FullName;
                    }
                    args.Add(this[parameterName]);
                }
                string id = type.FullName;
                Runtime runtimeInstance = Runtime.CreateInstance;
                Type referenceType = type;
                void load(MemberInfo mi)
                {
                    DependencyAttribute attribute = mi.GetCustomAttribute<DependencyAttribute>();
                    if (attribute != null)
                    {
                        Type instanceType = attribute.Type;
                        if (instanceType != null)
                        {
                            if (!referenceType.IsAssignableFrom(instanceType))
                            {
                                throw new Exception(string.Format("{0} is not assignable from to {1}", referenceType.FullName, instanceType.FullName));
                            }
                            id = attribute.Type.FullName;
                        }
                        else
                        {
                            if (attribute.Id != null)
                            {
                                id = attribute.Id;
                            }
                        }
                    }
                    else
                    {
                        id = referenceType.FullName;
                    }
                    runtimeInstance = attribute?.Runtime ?? Runtime.None;
                }
                load(type);
                load(constructor);
                if (d.ContainsKey(id))
                {
                    throw new Exception(string.Format("{0} amibiguity between constructors in type", id));
                }
                object init() => Activator.CreateInstance(type, args.Select(p => p.CreateInstance()).ToArray());
                this[id].Initialize(init, runtimeInstance, id);
            }
        }

        RuntimeType this[string type]
        {
            get
            {
                if (!d.ContainsKey(type))
                {
                    d.Add(type, new RuntimeType());
                }
                return d[type];
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
                throw new Exception(string.Format("{0} is not allowed type", typeof(T).FullName));
            }
            return (T)typeBuilder.CreateInstance(typeof(T));
        }

        public void RegisterType<T>()
        {
            if (!_filter(typeof(T)))
            {
                throw new Exception(string.Format("{0} is not allowed type", typeof(T).FullName));
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
