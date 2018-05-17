using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Runtime information for type
    /// </summary>
    /// <seealso cref="Build.IRuntimeType" />
    class RuntimeType : IRuntimeType
    {
        bool _guard;
        object _value;

        /// <summary>
        /// The values
        /// </summary>
        IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeType"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="type">The type.</param>
        /// <exception cref="ArgumentNullException">attribute</exception>
        public RuntimeType(IRuntimeAttribute attribute, RuntimeType parent, Type type)
        {
            Type = type;
            Parent = parent ?? this;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        /// <summary>
        /// Gets the type of the assignable.
        /// </summary>
        /// <value>
        /// The type of the assignable.
        /// </value>
        public Type AssignableType => AssignableTypes.Count == 0 ? Type : AssignableTypes[0];

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public IRuntimeAttribute Attribute { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id => Format.GetConstructorFullName(Type.FullName, RuntimeTypes.Select(p => p.AssignableType.FullName));

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized => RuntimeInstance != RuntimeInstance.Default;

        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        /// <value>
        /// The parameters count.
        /// </value>
        public int ParametersCount => RuntimeTypes.Count;

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public IRuntimeType Parent { get; }

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>
        /// The runtime instance.
        /// </value>
        public RuntimeInstance RuntimeInstance => RuntimeInstanceInternal;

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>
        /// The runtime parameters.
        /// </value>
        public IEnumerable<IRuntimeType> RuntimeParameters => RuntimeTypes;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; }

        /// <summary>
        /// Gets the assignable types.
        /// </summary>
        /// <value>
        /// The assignable types.
        /// </value>
        List<Type> AssignableTypes { get; } = new List<Type>();

        /// <summary>
        /// Gets or sets the runtime instance internal.
        /// </summary>
        /// <value>
        /// The runtime instance internal.
        /// </value>
        RuntimeInstance RuntimeInstanceInternal { get; set; }

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>
        /// The runtime types.
        /// </value>
        List<RuntimeType> RuntimeTypes { get; } = new List<RuntimeType>();

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified attribute.
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="attribute">The attribute.</param>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterRuntimeType">Type of the parameter runtime.</param>
        public void AddParameter(RuntimeType parameterRuntimeType) => RuntimeTypes.Add(parameterRuntimeType);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException">
        /// </exception>
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

        /// <summary>
        /// Finds the type of the assignable.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Type FindAssignableType(string id) => AssignableTypes.FirstOrDefault(p => p.FullName == id && p.IsAssignableFrom(Type));

        /// <summary>
        /// Initializes the specified runtime instance.
        /// </summary>
        /// <param name="runtimeInstance">The runtime instance.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        public void Initialize(RuntimeInstance runtimeInstance)
        {
            if (IsInitialized)
                throw new TypeRegistrationException(string.Format("{0} is not registered (more than one constructor available)", Type.FullName));
            RuntimeInstanceInternal = runtimeInstance;
        }

        /// <summary>
        /// Determines whether [is assignable from] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is assignable from] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAssignableFrom(string id) => FindAssignableType(id) != null;

        /// <summary>
        /// Registers the type of the assignable.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterAssignableType(Type type)
        {
            if (!AssignableTypes.Contains(type))
                AssignableTypes.Add(type);
            AssignableTypes.Sort(RuntimeTypeComparer.Instance);
        }

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object CreateInstance(IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p[attribute, Id, i]).ToArray());

        /// <summary>
        /// Evaluates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object Evaluate(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p.EvaluateRuntimeInstance(type, attribute, i)).ToArray());

        /// <summary>
        /// Evaluates the argument.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateArgument(IRuntimeAttribute attribute, int? i)
        {
            if (RuntimeInstance == RuntimeInstance.None)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", Type.FullName));
            return EvaluateInjection(attribute, i);
        }

        /// <summary>
        /// Evaluates the injection.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateInjection(IRuntimeAttribute attribute, int? i)
        {
            if (attribute is InjectionAttribute injection && injection.Args.Length > 0 && i.HasValue)
                return injection.Args[i.Value];
            return this[attribute, Id, i];
        }

        /// <summary>
        /// Evaluates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
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

        /// <summary>
        /// Evaluates the runtime instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Evaluates the singleton.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateSingleton(IRuntimeType type, int? i)
        {
            if (!_guard)
            {
                _value = EvaluateInstance(type, i);
                _guard = true;
            }
            return _value;
        }

        /// <summary>
        /// Matches the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        bool Match(object[] args) => (args.Length == 0 || RuntimeTypes == null || args.Length == RuntimeTypes.Count) && RegisterParameters(Id, args);
    }
}