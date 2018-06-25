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
            TypeDefinition = type.ToString();
            Type = type;
            UseDefaultTypeInstantiation = defaultTypeInstantiation;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            UpdateTypeId();
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
        /// True if parameters was initialized
        /// </summary>
        public bool GetInstance => _runtimeInstance == (_runtimeInstance | RuntimeInstance.GetInstance);

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized => _runtimeInstance != RuntimeInstance.None;

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
        public string TypeFullName => Type.ToString();

        public object Value => GetValue(Attribute, Id);

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        bool UseDefaultTypeInstantiation { get; }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> with the specified attribute.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="attribute">The attribute.</param>
        /// <param name="typeFullName">Full name of the type.</param>
        /// <returns></returns>
        object this[IRuntimeAttribute attribute, string typeFullName]
        {
            get
            {
                var runtimeAttribute = attribute.GetReferenceAttribute(typeFullName);
                if (!_values.ContainsKey(runtimeAttribute))
                    _values.Add(runtimeAttribute, GetDefaultValue());
                return _values[attribute];
            }
            set => _values[attribute.GetReferenceAttribute(typeFullName)] = value;
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterRuntimeType">Type of the parameter runtime.</param>
        public void AddConstructorParameter(bool canRegister, IRuntimeType parameterRuntimeType)
        {
            _runtimeTypes.Add(parameterRuntimeType);
            UpdateTypeId();
        }

        /// <summary>
        /// Determines whether the specified identifier is assignable from type.
        /// </summary>
        /// <param name="typeFullName">The identifier.</param>
        /// <returns>
        /// <c>true</c> if the specified identifier is assignable from type, otherwise <c>false</c>.
        /// </returns>
        public bool ContainsTypeDefinition(string typeFullName) => _typeDefinitions.FirstOrDefault(p => p == typeFullName) != null;

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(params object[] args)
        {
            if (Type.IsValueType)
                return CreateValueInstance();
            var parameters = ReadParameters();

            #region Target Frameworks

#if NET45 || NET451 || NET452
            var emptyArray = new object[0];
#else
            var emptyArray = Array.Empty<object>();
#endif

            #endregion

            var result = CreateReferenceType(args ?? emptyArray);
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
        public object Evaluate(IRuntimeType type, IRuntimeAttribute attribute, int? i)
        {
            switch (_runtimeInstance)
            {
                case RuntimeInstance.Singleton:
                case RuntimeInstance.Singleton | RuntimeInstance.GetInstance:
                    return EvaluateSingleton(type, i);

                case RuntimeInstance.CreateInstance:
                case RuntimeInstance.CreateInstance | RuntimeInstance.GetInstance:
                    return EvaluateInstance(type, i);

                default:
                    return EvaluateDefault(attribute, i);
            }
        }

        /// <summary>
        /// Gets the specified value from the specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="id">Id</param>
        /// <value>Value</value>
        public object GetValue(IRuntimeAttribute attribute, string id) => this[attribute, id];

        /// <summary>
        /// Gets the parameters;
        /// </summary>
        /// <returns></returns>
        public object[] ReadParameters()
        {
            object[] args = new object[_runtimeTypes.Count];
            for (int i = 0; i < _runtimeTypes.Count; i++)
            {
                args[i] = _runtimeTypes[i].GetValue(Attribute, Id);
            }
            return args;
        }

        /// <summary>
        /// Registers the specified arguments match search criteria.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public bool RegisterConstructorParameters(object[] args) => (args.Length == 0 || RuntimeTypes == null || args.Length == _runtimeTypes.Count) && WriteParameters(args);

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
        public void SetRuntimeInstance(RuntimeInstance runtimeInstance) => _runtimeInstance = _runtimeInstance | runtimeInstance;

        ///// <summary>
        ///// Sets the specified value to the specified attribute.
        ///// </summary>
        ///// <param name="attribute">Attribute</param>
        ///// <param name="id">Id</param>
        ///// <param name="value">Value</param>
        public void SetValue(IRuntimeAttribute attribute, string id, object value) => this[attribute, id] = value;

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        public bool WriteParameters(object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] != null && !_runtimeTypes[i].ContainsTypeDefinition(Format.GetParameterFullName(args[i])))
                    return false;
                _runtimeTypes[i].SetValue(Attribute, Id, args[i]);
            }
            return true;
        }

        /// <summary>
        /// Creates the instance with type inferenced evaluated arguments.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        object CreateInstance(IRuntimeType type, IRuntimeAttribute attribute) => Activator.CreateInstance(Type, RuntimeTypes.Evaluate(type, attribute));

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        object CreateInstance() => Activator.CreateInstance(Type, RuntimeTypes.Values(Attribute, Id));

        /// <summary>
        /// Creates reference type
        /// </summary>
        /// <param name="args">Parameter passed in to type activator</param>
        /// <returns>Returns instance of a reference type</returns>
        object CreateReferenceType(params object[] args)
        {
            if (!IsInitialized)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", TypeFullName));
            if (!RegisterConstructorParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", TypeFullName));
            if (args == null || args.Length == 0)
                return Evaluate(this, Attribute, null);
            return CreateInstance();
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        object CreateValueInstance() => Activator.CreateInstance(Type);

        /// <summary>
        /// Evaluates the injection.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateAttribute(IRuntimeAttribute attribute, int? i)
        {
            if (i.HasValue && attribute is IInjectionAttribute injection && injection.CheckBounds(i.Value))
                return injection.GetObject(i.Value);
            return GetValue(attribute, Id);
        }

        /// <summary>
        /// Evaluates the argument.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateDefault(IRuntimeAttribute attribute, int? i)
        {
            if (_runtimeInstance == RuntimeInstance.Exclude || (!UseDefaultTypeInstantiation && IsDefaultReferencedType()))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", TypeFullName));
            return EvaluateAttribute(attribute, i);
        }

        /// <summary>
        /// Evaluates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateInstance(IRuntimeType type, int? i) => CreateInstance(type, i.HasValue ? Attribute.GetReferenceAttribute(type.Id) : Attribute);

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
        /// Gets default value for type
        /// </summary>
        object GetDefaultValue()
        {
            if (Type.IsValueType)
            {
                if (Type.IsEnum)
                {
                    var enums = Enum.GetValues(Type);
                    if (enums.Length > 0)
                        return enums.GetValue(0);
                }
                return CreateValueInstance();
            }
            return default;
        }

        /// <summary>
        /// Checks whether type is not a value type and not yet initialized
        /// </summary>
        /// <returns></returns>
        bool IsDefaultReferencedType() => !Type.IsValueType && _runtimeInstance == RuntimeInstance.None;

        /// <summary>
        /// Updates runtime type id
        /// </summary>
        void UpdateTypeId() => Id = Format.GetConstructorWithParameters(TypeFullName, _runtimeTypes.Select(p => p.TypeDefinition));
    }
}