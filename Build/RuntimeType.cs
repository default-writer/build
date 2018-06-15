using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Runtime information for type
    /// </summary>
    /// <seealso cref="Build.IRuntimeType"/>
    public sealed class RuntimeType : IRuntimeType
    {
        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        readonly List<IRuntimeType> _runtimeTypes = new List<IRuntimeType>();

        /// <summary>
        /// Gets the assignable types.
        /// </summary>
        /// <value>The assignable types.</value>
        readonly List<string> _typeDefinitions = new List<string>();

        /// <summary>
        /// The values
        /// </summary>
        readonly IDictionary<IRuntimeAttribute, object> _values = new Dictionary<IRuntimeAttribute, object>();

        bool _instance;

        RuntimeInstance _runtimeInstance;

        object _value;

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
            TypeDefinition = type.FullName;
            Type = type;
            UseDefaultTypeInstantiation = defaultTypeInstantiation;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            Id = Format.GetConstructorWithParameters(TypeFullName, _runtimeTypes.Select(p => p.TypeDefinition));
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        public IRuntimeAttribute Attribute { get; }

        /// <summary>
        /// Gets the parameters count.
        /// </summary>
        /// <value>The parameters count.</value>
        public int Count => _runtimeTypes.Count;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the runtime parameters.
        /// </summary>
        /// <value>The runtime parameters.</value>
        public IEnumerable<IRuntimeType> RuntimeTypes => _runtimeTypes;

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; }

        /// <summary>
        /// Gets the first assignable type.
        /// </summary>
        /// <value>The type full name of the assignable.</value>
        public string TypeDefinition { get; private set; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        public string TypeFullName => Type.FullName;

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        public bool UseDefaultTypeInstantiation { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        bool IsInitialized => _runtimeInstance != RuntimeInstance.Default;

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified attribute.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="attribute">The attribute.</param>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns></returns>
        public object this[IRuntimeAttribute attribute, string typeFullName]
        {
            get
            {
                if (!_values.ContainsKey(attribute.GetRuntimeType(typeFullName)))
                    _values.Add(attribute.GetRuntimeType(typeFullName), null);
                return _values[attribute.GetRuntimeType(typeFullName)];
            }
            set => _values[attribute.GetRuntimeType(typeFullName)] = value;
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterRuntimeType">Type of the parameter runtime.</param>
        public void AddParameter(IRuntimeType parameterRuntimeType)
        {
            _runtimeTypes.Add(parameterRuntimeType);
            Id = Format.GetConstructorWithParameters(TypeFullName, _runtimeTypes.Select(p => p.TypeDefinition));
        }

        /// <summary>
        /// Determines whether the specified identifier is assignable from type.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// <c>true</c> if the specified identifier is assignable from type, otherwise <c>false</c>.
        /// </returns>
        public bool ContainsTypeDefinition(string id) => _typeDefinitions.FirstOrDefault(p => p == id) != null;

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
        /// Evaluates the runtime instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public object EvaluateRuntimeInstance(IRuntimeType type, IRuntimeAttribute attribute, int? i)
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

        /// <summary>
        /// Gets the parameters;
        /// </summary>
        /// <returns></returns>
        public object[] ReadParameters()
        {
            object[] args = new object[_runtimeTypes.Count];
            for (int i = 0; i < _runtimeTypes.Count; i++)
            {
                args[i] = _runtimeTypes[i][Attribute, Id];
            }
            return args;
        }

        /// <summary>
        /// Registers type full name as assignable type
        /// </summary>
        /// <param name="typeFullName">The type full name.</param>
        public void RegisterTypeDefinition(string typeFullName)
        {
            if (!_typeDefinitions.Contains(typeFullName))
            {
                _typeDefinitions.Add(typeFullName);
                TypeDefinition = typeFullName;
            }
        }

        /// <summary>
        /// Sets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public void SetRuntimeInstance(RuntimeInstance runtimeInstance) => _runtimeInstance = runtimeInstance;

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public bool WriteParameters(object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] != null && !_runtimeTypes[i].ContainsTypeDefinition(Format.GetParameterFullName(args[i])))
                    return false;
                _runtimeTypes[i][Attribute, Id] = args[i];
            }
            return true;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object CreateInstance(IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Select((p, i) => p[attribute, Id]).ToArray());

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
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", TypeFullName));
            if (!RegisterParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", TypeFullName));
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
            if (_runtimeInstance == RuntimeInstance.None || (!UseDefaultTypeInstantiation && IsDefaultReferencedType()))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", TypeFullName));
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
            if (i.HasValue && attribute is IInjectionAttribute injection && injection.CheckBounds(i.Value))
                return injection.GetObject(i.Value);
            return this[attribute, Id];
        }

        /// <summary>
        /// Evaluates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateInstance(IRuntimeType type, int? i) => CreateInstance(type, i.HasValue ? Attribute.GetRuntimeType(type.Id) : Attribute);

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
        /// Checks whether type is not a value type and not yet initialized
        /// </summary>
        /// <returns></returns>
        bool IsDefaultReferencedType() => !Type.IsValueType && _runtimeInstance == RuntimeInstance.Default;

        /// <summary>
        /// Registers the specified arguments match search criteria.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        bool RegisterParameters(object[] args) => (args.Length == 0 || RuntimeTypes == null || args.Length == _runtimeTypes.Count) && WriteParameters(args);
    }
}