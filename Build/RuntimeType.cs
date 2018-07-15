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
        /// The raw values
        /// </summary>
        readonly IDictionary<string, object> _objects = new Dictionary<string, object>();

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        readonly List<IRuntimeType> _runtimeTypes = new List<IRuntimeType>();

        /// <summary>
        /// Gets the assignable types.
        /// </summary>
        /// <value>The assignable types.</value>
        readonly HashSet<string> _types = new HashSet<string>();

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
        public RuntimeType(ITypeActivator typeActivator, IRuntimeAttribute attribute, Type type, bool defaultTypeInstantiation)
        {
            Activator = typeActivator ?? throw new ArgumentNullException(nameof(typeActivator));
            Type = (type ?? throw new ArgumentNullException(nameof(type))).ToString();
            ActivatorType = type ?? throw new ArgumentNullException(nameof(type));
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            UseDefaultTypeInstantiation = defaultTypeInstantiation;
            UpdateTypeId();
        }

        /// <summary>
        /// Gets the CLR type.
        /// </summary>
        /// <value>The type.</value>
        public Type ActivatorType { get; }

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
        /// Gets the last assignable type.
        /// </summary>
        /// <value>The type full name of the assignable.</value>
        public string Type { get; private set; }

        /// <summary>
        /// Gets the full name of hosted runtime type
        /// </summary>
        public string TypeFullName => ActivatorType.ToString();

        /// <summary>
        /// Assignable types
        /// </summary>
        public IEnumerable<string> Types => _types;

        public object Value => GetValue(Attribute, Id);

        /// <summary>
        /// IRuntimeType activator
        /// </summary>
        ITypeActivator Activator { get; }

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
                    return GetDefaultValue();
                return _values[attribute];
            }
            set => _values[attribute.GetReferenceAttribute(typeFullName)] = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/>.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="typeId">Object id.</param>
        /// <returns></returns>
        object this[string typeId]
        {
            get
            {
                if (!_objects.ContainsKey(typeId))
                    return default;
                return _objects[typeId];
            }
            set => _objects[typeId] = value;
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
        public bool ContainsTypeDefinition(string typeFullName) => _types.Contains(typeFullName);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance()
        {
            var parameters = ReadParameters();
            var result = CreateReferenceType(Array.Empty<object>());
            WriteParameters(parameters);
            return result;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(object[] args)
        {
            if (ActivatorType.IsValueType && ActivatorType.IsPrimitive)
                return Value;
            var parameters = ReadParameters();
            var result = CreateReferenceType(args);
            WriteParameters(parameters);
            return result;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(string[] args)
        {
            var parameters = ReadParameters();
            var result = CreateReferenceType(args);
            WriteParameters(parameters);
            return result;
        }

        /// <summary>
        /// Creates the value instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateValueInstance()
        {
            try
            {
                if (ActivatorType.IsPrimitive)
                    return Value;
                return Activator.CreateInstance(this);
            }
            catch (Exception ex)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated", Id), ex);
            }
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
                    return EvaluateSingleton(type, i.HasValue ? Attribute.GetReferenceAttribute(type.Id) : Attribute);

                case RuntimeInstance.CreateInstance:
                case RuntimeInstance.CreateInstance | RuntimeInstance.GetInstance:
                    return EvaluateInstance(type, i.HasValue ? Attribute.GetReferenceAttribute(type.Id) : Attribute);

                case RuntimeInstance.None:
                    if (UseDefaultTypeInstantiation || !IsDefaultReferencedType())
                        return EvaluateAttribute(attribute, i);
                    break;
            }
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (constructor not allowed)", TypeFullName));
        }

        /// <summary>
        /// Gets the value from the specified attribue.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="typeId">Id</param>
        /// <value>Value</value>
        public object GetValue(IRuntimeAttribute attribute, string typeId) => this[attribute, typeId];

        /// <summary>
        /// Gets the value from the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <value>Value</value>
        public object GetValue(string typeId) => this[typeId];

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
        /// Registers the specified arguments match search criteria.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public bool RegisterConstructorParameters(string[] args) => (args.Length == 0 || args.Length == _runtimeTypes.Count) && TryWriteParameters(args);

        /// <summary>
        /// Registers type full name as assignable type
        /// </summary>
        /// <param name="typeFullName">The type full name.</param>
        public void RegisterTypeDefinition(string typeFullName)
        {
            if (_types.Add(typeFullName))
                Type = typeFullName;
        }

        /// <summary>
        /// Sets the runtime instance.
        /// </summary>
        /// <value>The runtime instance.</value>
        public void SetRuntimeInstance(RuntimeInstance runtimeInstance) => _runtimeInstance = _runtimeInstance | runtimeInstance;

        /// <summary>
        /// Sets the value to the specified attribute.
        /// </summary>
        /// <param name="attribute">Attribute</param>
        /// <param name="typeId">Type id</param>
        /// <param name="value">Value</param>
        public void SetValue(IRuntimeAttribute attribute, string typeId, object value) => this[attribute, typeId] = value;

        /// <summary>
        /// Sets the value to the runtime type.
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <param name="value">Value</param>
        public void SetValue(string typeId, object value) => this[typeId] = value;

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        public bool TryWriteParameters(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
                if (args[i] != null && !_runtimeTypes[i].ContainsTypeDefinition(args[i]))
                    return false;
            return true;
        }

        /// <summary>
        /// Registers the parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns true if parameters has written successfully, otherwize, false</returns>
        public bool WriteParameters(object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] != null && !_runtimeTypes[i].ActivatorType.IsAssignableFrom(Format.GetType(args[i])))
                    return false;
                _runtimeTypes[i].SetValue(Attribute, Id, args[i]);
            }
            return true;
        }

        /// <summary>
        /// Creates reference type
        /// </summary>
        /// <param name="args">Parameter passed in to type activator</param>
        /// <returns>Returns instance of a reference type</returns>
        object CreateReferenceType(object[] args)
        {
            if (!IsInitialized)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", TypeFullName));
            if (!RegisterConstructorParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", TypeFullName));
            try
            {
                if (args == null || args.Length == 0)
                    return Evaluate(this, Attribute, null);
                return Activator.CreateInstance(this);
            }
            catch (Exception ex)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated", Id), ex);
            }
        }

        /// <summary>
        /// Creates reference type
        /// </summary>
        /// <param name="args">Parameter passed in to type activator</param>
        /// <returns>Returns instance of a reference type</returns>
        object CreateReferenceType(string[] args)
        {
            if (!IsInitialized)
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructor available)", TypeFullName));
            if (!RegisterConstructorParameters(args))
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (parameter type mismatch)", TypeFullName));
            try
            {
                if (args == null || args.Length == 0)
                    return Evaluate(this, Attribute, null);
                return Activator.CreateInstance(this);
            }
            catch (Exception ex)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated", Id), ex);
            }
        }

        /// <summary>
        /// Evaluates the injection.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        object EvaluateAttribute(IRuntimeAttribute attribute, int? i) => i.HasValue && attribute is IInjectionAttribute injection && injection.CheckBounds(i.Value) ? injection.GetObject(i.Value) : GetValue(attribute, Id);

        /// <summary>
        /// Evaluates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The runtime attrubute.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        object EvaluateInstance(IRuntimeType type, IRuntimeAttribute attribute)
        {
            try
            {
                return Activator.CreateInstance(this, type, attribute);
            }
            catch (Exception ex)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated", Id), ex);
            }
        }

        /// <summary>
        /// Evaluates the singleton.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The runtime attribute.</param>
        /// <returns></returns>
        object EvaluateSingleton(IRuntimeType type, IRuntimeAttribute attribute)
        {
            if (!_instance)
            {
                _value = EvaluateInstance(type, attribute);
                _instance = true;
            }
            return _value;
        }

        /// <summary>
        /// Gets default value for type
        /// </summary>
        object GetDefaultValue()
        {
            try
            {
                if (ActivatorType.IsValueType)
                {
                    if (ActivatorType.IsEnum)
                    {
                        var enums = Enum.GetValues(ActivatorType);
                        if (enums.Length > 0)
                            return enums.GetValue(0);
                    }
                    return Activator.CreateValueInstance(this);
                }
                return default;
            }
            catch (Exception ex)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated", Id), ex);
            }
        }

        /// <summary>
        /// Checks whether type is not a value type and not yet initialized
        /// </summary>
        /// <returns></returns>
        bool IsDefaultReferencedType() => !ActivatorType.IsValueType && _runtimeInstance == RuntimeInstance.None;

        /// <summary>
        /// Updates runtime type id
        /// </summary>
        void UpdateTypeId() => Id = Format.GetConstructor(TypeFullName, _runtimeTypes.Select(p => p.Type));
    }
}