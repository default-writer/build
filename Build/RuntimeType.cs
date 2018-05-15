using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    class RuntimeType : IRuntimeType
    {
        bool _guard;
        object _value;
        IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        public RuntimeType(IRuntimeAttribute attribute, RuntimeType parent, Type type)
        {
            Type = type;
            Parent = parent ?? this;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        public Type AssignableType => AssignableTypes.Count == 0 ? Type : AssignableTypes[0];

        public IRuntimeAttribute Attribute { get; }

        public string Id => Format.GetConstructorFullName(Type.FullName, RuntimeTypes.Select(p => p.AssignableType.FullName));

        public bool IsInitialized => RuntimeInstance != RuntimeInstance.Default;

        public int ParametersCount => RuntimeTypes.Count;

        public IRuntimeType Parent { get; }

        public RuntimeInstance RuntimeInstance => RuntimeInstanceInternal;

        public IEnumerable<IRuntimeType> RuntimeParameters => RuntimeTypes;

        public Type Type { get; }

        List<Type> AssignableTypes { get; } = new List<Type>();

        RuntimeInstance RuntimeInstanceInternal { get; set; }

        List<RuntimeType> RuntimeTypes { get; } = new List<RuntimeType>();

        object this[IRuntimeAttribute attribute, string typeFullName, int? i]
        {
            get
            {
                var id = Format.GetConstructorParameterFullName(typeFullName, i);
                if (!_values.ContainsKey(attribute.GetRuntimeType(id)))
                    _values.Add(attribute.GetRuntimeType(id), null);
                return _values[attribute.GetRuntimeType(id)];
            }
            set
            {
                var id = Format.GetConstructorParameterFullName(typeFullName, i);
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
                return EvaluateRuntimeInstance(this, Attribute, null);
            return CreateInstance(Attribute);
        }

        public Type FindAssignableType(string id) => AssignableTypes.FirstOrDefault(p => p.FullName == id && p.IsAssignableFrom(Type));

        public void Initialize(RuntimeInstance runtimeInstance)
        {
            if (IsInitialized)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", Type.FullName));
            RuntimeInstanceInternal = runtimeInstance;
        }

        public bool IsAssignableFrom(string id) => FindAssignableType(id) != null;

        public void RegisterAssignableType(Type type)
        {
            if (!AssignableTypes.Contains(type))
                AssignableTypes.Add(type);
            AssignableTypes.Sort(RuntimeTypeComparer.Instance);
        }

        public bool RegisterParameters(string typeFullName, params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var parameterType = RuntimeTypes[i].Type;
                var runtimeType = (args[i] ?? typeof(object)).GetType();
                if (args[i] != null && !parameterType.IsAssignableFrom(runtimeType))
                    return false;
                RuntimeTypes[i][Attribute, typeFullName, i] = args[i];
            }
            return true;
        }

        object CreateInstance(IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p[attribute, Id, i]).ToArray());

        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p.EvaluateRuntimeInstance(type, attribute, i)).ToArray());

        object EvaluateArgument(IRuntimeAttribute attribute, int? i)
        {
            if (RuntimeInstance == RuntimeInstance.None)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", Type.FullName));
            return EvaluateInjection(attribute, i);
        }

        object EvaluateInjection(IRuntimeAttribute attribute, int? i)
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
            var runtimeAttribute = i.HasValue ? Attribute.GetRuntimeType(Format.GetConstructorParameterFullName(type.Id, i)) : null;
            result = Evaluate(type, runtimeAttribute ?? Attribute);
            _guard = false;
            return result;
        }

        object EvaluateRuntimeInstance(IRuntimeType type, IRuntimeAttribute attribute, int? i)
        {
            switch (RuntimeInstance)
            {
                case RuntimeInstance.Singleton:
                    return EvaluateSingleton(type, i);

                case RuntimeInstance.CreateInstance:
                    return EvaluateInstance(type, i);

                default:
                    return EvaluateArgument(attribute, i);
            }
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