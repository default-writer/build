using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public interface IRuntimeType
    {
        string Id { get; }
        IRuntimeType[] RuntimeParameters { get; }
        bool IsAssignableFrom(string typeId);
    }
    class RuntimeType : IRuntimeType
    {
        readonly Type _type;
        readonly HashSet<Type> _types = new HashSet<Type>();
        bool _init;
        bool _guard;
        Func<Type, object> _func;
        RuntimeInstance _runtime;
        RuntimeType[] _args = new RuntimeType[0];
        IDictionary<Type, object> _values = new Dictionary<Type, object>();
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
        public object this[Type type]
        {
            get
            {
                if (!_values.ContainsKey(type))
                    _values.Add(type, null);
                return _values[type];
            }
            set => _values[type] = value;
        }
        object Evaluate(Type type) => Activator.CreateInstance(_type, _args.Select(p => p.Create(type)).ToArray());
        object Call(Type type) => Activator.CreateInstance(_type, _args.Select(p => p[type]).ToArray());
        object Create(Type type)
        {
            object Evaluate()
            {
                if (_guard)
                    throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", _type.FullName));
                _guard = true;
                object result = _func(type);
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
        public object CreateInstance(Type runtime, params object[] args)
        {
            if (!_init)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", _type.FullName));
            if (!RegisterParameters(runtime, args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameters mismatch)", _type.FullName));
            if (args.Length == 0)
                return Create(runtime);
            return Call(runtime);
        }
        public bool RegisterParameters(Type type, params object[] args)
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
                    _args[i][type] = args[i];
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
