using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    class RuntimeType : IRuntimeType
    {
        Func<IRuntimeType, IRuntimeAttribute, object> _func;
        bool _guard;
        RuntimeInstance _runtimeInstance;
        object _value;
        IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        public RuntimeType(IRuntimeAttribute attribute, RuntimeType parent, Type type, params object[] args)
        {
            Type = type;
            Args = args;
            Parent = parent ?? this;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        public object[] Args { get; }

        public IRuntimeAttribute Attribute { get; }

        public string Id => string.Format("{0}({1})", Type.FullName, string.Join(",", RuntimeTypes.Select(p => p.Types[0].FullName)));

        public bool IsInitialized => RuntimeInstance.None != _runtimeInstance;

        public IRuntimeType Parent { get; }

        public IRuntimeType[] RuntimeParameters => RuntimeTypes == null ? Array.Empty<RuntimeType>() : RuntimeTypes.ToArray();

        public Type Type { get; private set; }

        List<RuntimeType> RuntimeTypes { get; } = new List<RuntimeType>();

        List<Type> Types { get; } = new List<Type>();

        object this[IRuntimeAttribute attribute, string typeFullName, int? i]
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

        public void AddParameter(RuntimeType parameterRuntimeType) => RuntimeTypes.Add(parameterRuntimeType);

        public object CreateInstance(params object[] args)
        {
            if (!IsInitialized)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", Type.FullName));
            if (ParametersMismatch())
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter mismatch)", Type.FullName));
            if (!RegisterParameters(Id, args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", Type.FullName));
            if (args.Length == 0)
                return Create(this, Attribute, null);
            return Call(Attribute);
        }

        public Type FindParameterType(string id) => Types.FirstOrDefault(p => p.FullName == id);

        public void Initialize(RuntimeInstance runtimeInstance)
        {
            if (IsInitialized)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", Type.FullName));
            _runtimeInstance = runtimeInstance;
            _func = Evaluate;
        }

        public bool IsAssignableFrom(string id)
        {
            var runtimeType = Type;
            var parameterType = FindParameterType(id);
            return parameterType != null && parameterType.IsAssignableFrom(runtimeType);
        }

        public bool RegisterParameters(string typeFullName, params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var parameterType = RuntimeTypes[i].Type;
                var runtimeType = args[i] == null ? typeof(object) : args[i].GetType();
                if (args[i] != null && !parameterType.IsAssignableFrom(runtimeType))
                    return false;
                RuntimeTypes[i][Attribute, typeFullName, i] = args[i];
            }
            return true;
        }

        public void RegisterType(Type type)
        {
            if (!Types.Contains(type))
                Types.Add(type);
            Types.Sort(RuntimeTypeComparer.Instance);
        }

        object Call(IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p[attribute, Id, i]).ToArray());

        object Create(IRuntimeType type, IRuntimeAttribute attribute, int? i)
        {
            switch (_runtimeInstance)
            {
                case RuntimeInstance.Singleton:
                    return EvaluateSingleton(type, i);

                case RuntimeInstance.CreateInstance:
                    return EvaluateInstance(type, i);

                default:
                    if (_func != null)
                        throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", Type.FullName));
                    return EvaluateArgument(attribute, i);
            }
        }

        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p.Create(type, attribute, i)).ToArray());

        object EvaluateArgument(IRuntimeAttribute attribute, int? i)
        {
            if (attribute is InjectionAttribute injection && injection.Args.Length > 0 && i.HasValue)
                return injection.Args[i.Value];
            return this[attribute, Id, i];
        }

        object EvaluateInstance(IRuntimeType type, int? i)
        {
            if (_guard)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", Type.FullName));
            _guard = true;
            object result = null;
            var runtimeAttribute = i.HasValue ? Attribute.GetRuntimeType(string.Format("{0}:({1})", type.Id, i)) : null;
            if (runtimeAttribute != null)
            {
                result = _func(type, runtimeAttribute);
            }
            else
            {
                result = _func(type, Attribute);
            }
            _guard = false;
            return result;
        }

        object EvaluateSingleton(IRuntimeType type, int? i)
        {
            if (!_guard)
            {
                _value = EvaluateInstance(type, i);
                _guard = true;
            }
            return _value;
        }

        bool ParametersMismatch() => (RuntimeTypes != null && Args == null && RuntimeTypes.Count > 0) || (RuntimeTypes == null && Args != null && Args.Length > 0);
    }
}