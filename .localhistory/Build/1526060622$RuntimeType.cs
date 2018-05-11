using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Build
{
    public interface IRuntimeType
    {
        IRuntimeAttribute Attribute { get; }
        string RuntimeTypeId { get; }
        IRuntimeType[] RuntimeParameters { get; }
        //IRuntimeType Runtime { get; }
        bool IsAssignableFrom(string typeId);
    }
    class RuntimeType : IRuntimeType
    {
        object[] _parameters;
        List<Type> _types = new List<Type>(/*new Type[1] { typeof(object) }*/);
        bool _init;
        bool _guard;
        Func<RuntimeType, object> _func;
        IRuntimeAttribute _attribute;
        IRuntimeType _parent;
        RuntimeInstance _runtimeInstance;
        Type _type;
        List<RuntimeType> _args = new List<RuntimeType>();
        IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();
        public IEnumerable<Type> Parameters => _types;
        public Type Type => _type;
        public bool IsInitialized => _init;
        public string RuntimeTypeId => string.Format("{0}", _type.FullName);
        public IRuntimeType Parent => _parent;
        public IRuntimeType[] RuntimeParameters => _args == null ? new RuntimeType[0] : _args.ToArray();
        public IRuntimeAttribute Attribute => _attribute;

        public RuntimeType(IRuntimeAttribute attribute, RuntimeType parent, Type type, params object[] args)
        {
            _type = type;
            _parameters = args;
            _parent = parent ?? this;
            _attribute = attribute;
        }
        public void Initialize(RuntimeInstance runtimeInstance, Type type)
        {
            if (_init)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", _type.FullName));
            _runtimeInstance = runtimeInstance;
            _type = type;
            _func = Evaluate;
            _init = true;
        }
        class RuntimeTypeComparer : IComparer<Type>
        {
            private RuntimeTypeComparer() { }
            public int Compare(Type x, Type y) => y.IsAssignableFrom(x) ? 1 : -1;
            public static IComparer<Type> Instance { get; } = new RuntimeTypeComparer();
        }
        public void RegisterType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
            _types.Sort(RuntimeTypeComparer.Instance);
        }
        public object this[IRuntimeAttribute attribute, string typeFullName, int? i]
        {
            get
            {
                var typeId = string.Format("{0}:({1})", typeFullName, i);
                if (!_values.ContainsKey(attribute.GetRuntimeType(typeId)))
                    _values.Add(attribute.GetRuntimeType(typeId), null);
                return _values[attribute.GetRuntimeType(typeId)];
            }
            set
            {
                var typeId = string.Format("{0}:({1})", typeFullName, i);
                //Debug.WriteLine("{0} Attribute {1} Value {2}", this, attribute, value);
                _values[attribute.GetRuntimeType(typeId)] = value;
            }
        }
        object Evaluate(RuntimeType type) => Activator.CreateInstance(_type, _args.Select((p, i) => p.Create(type, Id, i)).ToArray());
        object Call(RuntimeType type) => Activator.CreateInstance(_type, _args.Select((p, i) => p[type.Attribute, Id, i]).ToArray());
        object Create(RuntimeType type, string typeId, int? i)
        {
            object Evaluate()
            {
                if (_guard)
                    throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", _type.FullName));
                _guard = true;
                object result = _func(this);
                _guard = false;
                return result;
            }
            switch (_runtimeInstance)
            {
                case RuntimeInstance.Singleton:
                    if (!_guard)
                    {
                        type[this.Attribute, this.Id, i] = Evaluate();
                        _guard = true;
                    }
                    return type[this.Attribute, this.Id, i];
                case RuntimeInstance.CreateInstance:
                    return Evaluate();
                case RuntimeInstance.None:
                default:
                    if (_func != null)
                        throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", _type.FullName));
                    return this[type.Attribute, type.Id, i];
            }
        }
        public object CreateInstance(IRuntimeType type, params object[] args)
        {
            if (!_init)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", _type.FullName));
            if (!RegisterParameters(type.Attribute, Id, args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameters mismatch)", _type.FullName));
            if (args.Length == 0)
                return Create((RuntimeType)type, Id, null);
            return Call((RuntimeType)type);
        }
        public bool RegisterParameters(IRuntimeAttribute attribute, string typeFullName, params object[] args)
        {
            if (_args != null && args == null && _args.Count > 0)
                return false;
            if (_args != null && args != null && _args.Count == args.Length)
                for (int i = 0; i < args.Length; i++)
                {
                    var parameterType = _args[i]._type;
                    var runtimeType = args[i] == null ? typeof(object) : args[i].GetType();
                    if (args[i] != null && !parameterType.IsAssignableFrom(runtimeType))
                        return false;
                    //_args[i][type/*_attribute*/] = args[i];
                    _args[i][attribute, typeFullName, i] = args[i];
                }
            return true;
        }
        //public object GetValue(RuntimeType type) => _values[type.Attribute];
        public override string ToString() => string.Format("Runtime {0}({1}) Attribute {2}", _type.FullName, string.Join(",", _args.Select(p => p.Type.FullName)), _attribute);
        public string Id => string.Format("{0}({1})", _type.FullName, string.Join(",", _args.Select(p => p.Type.FullName)));
        public Type FindParameterType(string typeId) => _types.FirstOrDefault(p => p.FullName == typeId);
        public bool IsAssignableFrom(string typeId)
        {
            var runtimeType = _type;
            var parameterType = FindParameterType(typeId);
            return parameterType != null && parameterType.IsAssignableFrom(runtimeType);
        }
        public void AddParameter(RuntimeType parameterRuntimeType) => _args.Add(parameterRuntimeType);
    }
}
