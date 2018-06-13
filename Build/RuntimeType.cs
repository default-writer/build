using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Runtime information for type
    /// </summary>
    /// <seealso cref="Build.IRuntimeType"/>
    class RuntimeType : IRuntimeType
    {
        /// <summary>
        /// The values
        /// </summary>
        readonly IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        bool _instance;

        RuntimeInstance _runtimeInstance;

        object _value;

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        bool DefaultTypeInstantiation { get; }

        /// <summary>
        /// Gets the type of the assignable.
        /// </summary>
        /// <value>The type of the assignable.</value>
        Type AssignableType;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeType"/> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="type">The type.</param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        /// <exception cref="ArgumentNullException">attribute</exception>
        public RuntimeType(IRuntimeAttribute attribute, Type type, bool defaultTypeInstantiation)
        {
            AssignableType = type;
            Type = type;
            DefaultTypeInstantiation = defaultTypeInstantiation;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        public IRuntimeAttribute Attribute { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id => Format.GetConstructorFullName(Type.FullName, RuntimeTypes.Select(p => p.AssignableType.FullName));

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized => RuntimeInstance != RuntimeInstance.Default;

        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        /// <value>The parameters count.</value>
        public int ParametersCount => RuntimeTypes.Count;

        /// <summary>
        /// Gets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public RuntimeInstance RuntimeInstance => _runtimeInstance;

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>The runtime parameters.</value>
        public IEnumerable<IRuntimeType> RuntimeParameters => RuntimeTypes;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        public string FullName => Type.FullName;

        /// <summary>
        /// Gets the assignable types.
        /// </summary>
        /// <value>The assignable types.</value>
        List<Type> AssignableTypes { get; } = new List<Type>();

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        List<RuntimeType> RuntimeTypes { get; } = new List<RuntimeType>();

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified attribute.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
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
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(params object[] args)
        {
            if (Type.IsValueType)
                return CreateInstance();
            var parameters = ReadParameters();
            var result = CreateReferenceType(args ?? Array.Empty<object>());
            WriteParameters(parameters);
            return result;
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
        public void Initialize(RuntimeInstance runtimeInstance) => _runtimeInstance = runtimeInstance;

        /// <summary>
        /// Determines whether [is assignable from] [the specified identifier].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// <c>true</c> if [is assignable from] [the specified identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAssignableFrom(string id) => FindAssignableType(id) != null;

        /// <summary>
        /// Gets the parameters;
        /// </summary>
        /// <returns></returns>
        public object[] ReadParameters()
        {
            object[] args = new object[RuntimeTypes.Count];
            for (int i = 0; i < RuntimeTypes.Count; i++)
            {
                args[i] = RuntimeTypes[i][Attribute, Id, i];
            }
            return args;
        }

        /// <summary>
        /// Registers the type of the assignable.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterAssignableType(Type type)
        {
            if (!AssignableTypes.Contains(type))
            {
                AssignableTypes.Add(type);
                AssignableType = type;
            }
        }

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public bool WriteParameters(object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var parameterType = RuntimeTypes[i].Type;
                var runtimeType = (args[i] ?? typeof(object)).GetType();
                if (args[i] != null && !parameterType.IsAssignableFrom(runtimeType))
                    return false;
                RuntimeTypes[i][Attribute, Id, i] = args[i];
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
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        object CreateInstance() => Activator.CreateInstance(Type);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object CreateInstance(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p.EvaluateRuntimeInstance(type, attribute, i)).ToArray());

        /// <summary>
        /// Creates reference type
        /// </summary>
        /// <param name="args">Parameter passed in to type activator</param>
        /// <returns>Returns instance of a reference type</returns>
        object CreateReferenceType(object[] args)
        {
            if (!IsInitialized)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", Type.FullName));
            if (!RegisterParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", Type.FullName));
            if (args.Length == 0)
                return EvaluateRuntimeInstance(this, Attribute, null);
            return CreateInstance(Attribute);
        }

        /// <summary>
        /// Evaluates the argument.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateArgument(IRuntimeAttribute attribute, int? i)
        {
            if (RuntimeInstance == RuntimeInstance.None || (!DefaultTypeInstantiation && IsDefaultReferencedType()))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", Type.FullName));
            return EvaluateInjection(attribute, i);
        }

        bool IsDefaultReferencedType() => !Type.IsValueType && RuntimeInstance == RuntimeInstance.Default;

        /// <summary>
        /// Evaluates the injection.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateInjection(IRuntimeAttribute attribute, int? i)
        {
            if (attribute is InjectionAttribute injection && injection.Arguments.Length > 0 && i.HasValue)
                return injection.Arguments[i.Value];
            return this[attribute, Id, i];
        }

        /// <summary>
        /// Evaluates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateInstance(IRuntimeType type, int? i) => CreateInstance(type, i.HasValue ? Attribute.GetRuntimeType(Format.GetConstructorParameterFullName(type.Id, i)) : Attribute);

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
            if (!_instance)
            {
                _value = EvaluateInstance(type, i);
                _instance = true;
            }
            return _value;
        }

        /// <summary>
        /// Registers the specified arguments match search criteria.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        bool RegisterParameters(object[] args) => (args.Length == 0 || RuntimeTypes == null || args.Length == RuntimeTypes.Count) && WriteParameters(args);
    }
}