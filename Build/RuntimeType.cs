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

        public bool IsInitialized => _runtimeInstance != RuntimeInstance.None;

        public int ParametersCount => RuntimeTypes.Count;

        public IRuntimeType Parent { get; }

        public IEnumerable<IRuntimeType> RuntimeParameters => RuntimeTypes;

        public Type Type { get; }

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
            if (!Match(args))
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
                var runtimeType = GetRuntimeType(args, i);
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

        static Type GetRuntimeType(object[] args, int i) => args[i] == null ? typeof(object) : args[i].GetType();

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
                    return EvaluateArgument(attribute, i);
            }
        }

        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p.Create(type, attribute, i)).ToArray());

        object Evaluate(IRuntimeAttribute attribute, int? i)
        {
            if (attribute is InjectionAttribute injection && injection.Args.Length > 0 && i.HasValue)
                return injection.Args[i.Value];
            return this[attribute, Id, i];
        }

        object EvaluateArgument(IRuntimeAttribute attribute, int? i)
        {
            if (_func != null)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", Type.FullName));
            return Evaluate(attribute, i);
        }

        object EvaluateInstance(IRuntimeType type, int? i)
        {
            if (_guard)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (circular references found)", Type.FullName));
            _guard = true;
            object result = null;
            var runtimeAttribute = i.HasValue ? Attribute.GetRuntimeType(string.Format("{0}:({1})", type.Id, i)) : null;
            result = _func(type, runtimeAttribute ?? Attribute);
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

        bool Match(object[] args) => args.Length == 0 || RuntimeTypes == null || args.Length == RuntimeTypes.Count && RegisterParameters(Id, args);
    }
}