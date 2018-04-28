using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public interface IRuntimeType
    {
        string Id { get; }
        IRuntimeType[] RuntimeParameters { get; }
        object CreateInstance(IRuntimeType runtimeType, params object[] args);
        bool IsAssignableFrom(string typeId);
    }
    class RuntimeType : IRuntimeType
    {
        readonly Type _type;
        readonly HashSet<Type> _types = new HashSet<Type>();
        bool _init;
        bool _guard;
        Func<object> _func;
        RuntimeInstance _runtime;
        RuntimeType[] _args = new RuntimeType[0];
        public IEnumerable<Type> Parameters => _types; 
        public Type Type => _type;
        public bool IsRegistered => _init;
        public string Id => _type.FullName;
        public IRuntimeType[] RuntimeParameters => _args;
        public RuntimeType(Type type) { _type = type; }
        public void RegisterRuntimeType(RuntimeInstance runtime, RuntimeType[] args)
        {
            if (_init)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", _type.FullName));
            _runtime = runtime;
            _args = args;
            _func = Evaluate;
            _init = true;
        }
        public void RegisterType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
        }
        IDictionary<IRuntimeType, object> _values = new Dictionary<IRuntimeType, object>();
        public object this[RuntimeType runtimeType]
        {
            get
            {
                if (!_values.ContainsKey(runtimeType))
                    _values.Add(runtimeType, null);
                return _values[runtimeType];
            }
            set => _values[runtimeType] = value;
        }
        object Evaluate() => Activator.CreateInstance(_type, _args.Select(p => p.Create(this)).ToArray());
        object Call() => Activator.CreateInstance(_type, _args.Select(p => p[this]).ToArray());
        object Create(RuntimeType type)
        {
            object Evaluate()
            {
                if (_guard)
                    throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", _type.FullName));
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
                        this[type] = Evaluate();
                        _guard = true;
                    }
                    return this[type];
                case RuntimeInstance.CreateInstance:
                    return Evaluate();
                case RuntimeInstance.None:
                default:
                    if (_func != null)
                        throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", _type.FullName));
                    return this[type];
            }
        }
        public object CreateInstance(IRuntimeType runtimeType, params object[] args)
        {
            if (!_init)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", _type.FullName));
            if (!RegisterParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameters mismatch)", _type.FullName));
            if (args.Length == 0)
                return Create(this);
            return Call();
        }
        public bool RegisterParameters(params object[] args)
        {
            if (_args != null && _args.Length > 0 && args == null)
                return false;
            if (args != null && _args.Length == args.Length)
                for (int i = 0; i < args.Length; i++)
                {
                    var parameterType = _args[i]._type;
                    var runtimeType = args[i]?.GetType();
                    if (args[i] != null && !parameterType.IsAssignableFrom(runtimeType))
                        return false;
                    _args[i][this] = args[i];
                }
            return true;
        }
        public override string ToString() => _args == null ? string.Format("{0}({1})", _type.FullName, string.Join(", ", _args.Select(p => p.Id))) : string.Format("{0}({1})", _type.FullName, string.Join(", ", _args.Select(p => p.Id)));
        public Type FindParameterType(string typeId) => _types.FirstOrDefault(p => p.FullName == typeId);
        public bool IsAssignableFrom(string typeId)
        {
            var runtimeType = _type;
            var parameterType = FindParameterType(typeId);
            return parameterType != null && parameterType.IsAssignableFrom(runtimeType);
        }
    }
}
