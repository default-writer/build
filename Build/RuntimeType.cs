using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    class RuntimeType : IRuntimeType

    {
        readonly List<RuntimeType> _args = new List<RuntimeType>();
        readonly object[] _parameters;
        readonly IRuntimeType _parent;
        IRuntimeAttribute _attribute;
        Func<IRuntimeType, IRuntimeAttribute, object> _func;
        bool _guard;
        bool _init;
        RuntimeInstance _runtimeInstance;
        Type _type;
        List<Type> _types = new List<Type>();
        object _value;
        IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        public RuntimeType(IRuntimeAttribute attribute, RuntimeType parent, Type type, params object[] args)
        {
            _type = type;
            _parameters = args;
            _parent = parent ?? this;
            _attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        public IRuntimeAttribute Attribute => _attribute;

        public string Id => string.Format("{0}({1})", _type.FullName, string.Join(",", _args.Select(p => p._types[0].FullName)));

        public bool IsInitialized => _init;

        public IEnumerable<Type> Parameters => _types;

        public IRuntimeType Parent => _parent;

        public IRuntimeType[] RuntimeParameters => _args == null ? Array.Empty<RuntimeType>() : _args.ToArray();

        public Type Type => _type;

        public object this[IRuntimeAttribute attribute, string typeFullName, int? i]
        {
            get
            {
                var id = string.Format("{0}:({1})", typeFullName, i);
                if (!_values.ContainsKey(attribute.GetRuntimeType(id)))
                    _values.Add(attribute.GetRuntimeType(id), null);
                return _values[attribute.GetRuntimeType(id)];
            }
            set
            {
                var id = string.Format("{0}:({1})", typeFullName, i);
                _values[attribute.GetRuntimeType(id)] = value;
            }
        }

        public void AddParameter(RuntimeType parameterRuntimeType) => _args.Add(parameterRuntimeType);

        public object CreateInstance(params object[] args)
        {
            if (!_init)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", _type.FullName));
            if (!RegisterParameters(Id, args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameters mismatch)", _type.FullName));
            if (args.Length == 0)
                return Create(this, _attribute, null);
            return Call(_attribute);
        }

        public Type FindParameterType(string id) => _types.FirstOrDefault(p => p.FullName == id);

        public void Initialize(RuntimeInstance runtimeInstance, Type type)
        {
            if (_init)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", _type.FullName));
            _runtimeInstance = runtimeInstance;
            _type = type;
            _func = Evaluate;
            _init = true;
        }

        public bool IsAssignableFrom(string id)
        {
            var runtimeType = _type;
            var parameterType = FindParameterType(id);
            return parameterType != null && parameterType.IsAssignableFrom(runtimeType);
        }

        public bool RegisterParameters(string typeFullName, params object[] args)
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
                    _args[i][_attribute, typeFullName, i] = args[i];
                }
            return true;
        }

        public void RegisterType(Type type)
        {
            if (!_types.Contains(type))
                _types.Add(type);
            _types.Sort(RuntimeTypeComparer.Instance);
        }

        object Call(IRuntimeAttribute attribute) => Activator.CreateInstance(_type, _args.Select((p, i) => p[attribute, Id, i]).ToArray());

        object Create(IRuntimeType type, IRuntimeAttribute attribute, int? i)
        {
            switch (_runtimeInstance)
            {
                case RuntimeInstance.Singleton:
                    if (!_guard)
                    {
                        _value = Evaluate(type, i);
                        _guard = true;
                    }
                    return _value;

                case RuntimeInstance.CreateInstance:
                    return Evaluate(type, i);

                default:
                    if (_func != null)
                        throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", _type.FullName));
                    var injection = attribute as InjectionAttribute;
                    if (injection != null && injection.Args.Length > 0 && i.HasValue)
                        return injection.Args[i.Value];
                    return this[attribute, Id, i];
            }
        }

        object Evaluate(IRuntimeType type, int? i)
        {
            if (_guard)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", _type.FullName));
            _guard = true;
            object result = null;
            var runtimeAttribute = i.HasValue ? _attribute.GetRuntimeType(string.Format("{0}:({1})", type.Id, i)) : null;
            if (runtimeAttribute != null)
            {
                result = _func(type, runtimeAttribute);
            }
            else
            {
                result = _func(type, _attribute);
            }
            _guard = false;
            return result;
        }

        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(_type, _args.Select((p, i) => p.Create(type, attribute, i)).ToArray());
    }
}