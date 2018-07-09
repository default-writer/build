using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    /// <summary>
    /// Type builder
    /// </summary>
    public sealed class TypeBuilder : ITypeBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
        /// <param name="options">Type builder options</param>
        public TypeBuilder(TypeBuilderOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            UseDefaultTypeResolution = options.UseDefaultTypeResolution ?? true;
            UseDefaultTypeInstantiation = options.UseDefaultTypeInstantiation ?? true;
            UseDefaultTypeAttributeOverwrite = options.UseDefaultTypeAttributeOverwrite ?? true;
            Activator = options.Activator ?? new TypeActivator();
            Constructor = options.Constructor ?? new TypeConstructor();
            Filter = options.Filter ?? new TypeFilter();
            Resolver = options.Resolver ?? new TypeResolver();
            Parser = options.Parser ?? new TypeParser();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        public TypeBuilder(ITypeActivator typeActivator, ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver)
        {
            Activator = typeActivator ?? throw new ArgumentNullException(nameof(typeActivator));
            Constructor = typeConstructor ?? throw new ArgumentNullException(nameof(typeConstructor));
            Filter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            Resolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            Parser = typeParser ?? throw new ArgumentNullException(nameof(typeParser));
        }

        /// <summary>
        /// Creates an instance of the specified runtime type
        /// </summary>
        public ITypeActivator Activator { get; }

        /// <summary>
        /// Constructs type dependency
        /// </summary>
        public ITypeConstructor Constructor { get; }

        /// <summary>
        /// Gets type filter
        /// </summary>
        /// <value>The filter</value>
        public ITypeFilter Filter { get; }

        /// <summary>
        /// Gets the status of type builder
        /// </summary>
        public bool IsLocked => Locked;

        /// <summary>
        /// Gets type parser
        /// </summary>
        /// <value>The parser</value>
        public ITypeParser Parser { get; }

        /// <summary>
        /// Gets type resolver
        /// </summary>
        /// <value>The resolver</value>
        public ITypeResolver Resolver { get; }

        /// <summary>
        /// Gets the runtime aliased types.
        /// </summary>
        /// <value>The type aliases.</value>
        public IEnumerable<string> RuntimeAliasedTypes => Types.Where(p => p.Key != p.Value.Id).Select(p => p.Value.Id);

        /// <summary>
        /// Gets the runtime non aliased types.
        /// </summary>
        /// <value>The runtime non aliased types.</value>
        public IEnumerable<string> RuntimeNonAliasedTypes => Types.Where(p => p.Key == p.Value.Id).Select(p => p.Value.Id);

        /// <summary>
        /// Gets the runtime aliases.
        /// </summary>
        /// <value>The runtime aliases.</value>
        public IEnumerable<string> RuntimeTypeAliases => Types.Where(p => p.Key != p.Value.Id).Select(p => p.Key);

        /// <summary>
        /// Gets the runtime types.
        /// </summary>
        /// <value>The runtime types.</value>
        public IEnumerable<string> RuntimeTypes => Types.Select(p => p.Value.Id);

        /// <summary>
        /// Gets all type invariants
        /// </summary>
        public IDictionary<string, IRuntimeType> TypeInvariants { get; } = new Dictionary<string, IRuntimeType>();

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        public IDictionary<string, IRuntimeType> Types { get; } = new Dictionary<string, IRuntimeType>();

        /// <summary>
        /// If locked returns true
        /// </summary>
        bool Locked { get; set; }

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        bool UseDefaultTypeAttributeOverwrite { get; } = true;

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        bool UseDefaultTypeInstantiation { get; } = true;

        /// <summary>
        /// True if automatic type resolution for reference types option enabled (does not throws
        /// exceptions for reference types contains type dependencies to non-registered types)
        /// </summary>
        /// <remarks>
        /// If automatic type resolution for reference types is enabled, type will defaults to null
        /// if not resolved and no exception will be thrown
        /// </remarks>
        bool UseDefaultTypeResolution { get; } = true;

        /// <summary>
        /// List the visited types.
        /// </summary>
        /// <value>The visited.</value>
        List<Type> Visited { get; } = new List<Type>();

        /// <summary>
        /// Gets the <see cref="RuntimeType"/> with the specified identifier.
        /// </summary>
        /// <value>The <see cref="RuntimeType"/>.</value>
        /// <param name="typeFullName">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IRuntimeType this[string typeFullName, IRuntimeType type]
        {
            get
            {
                if (!Types.ContainsKey(typeFullName))
                    Types.Add(typeFullName, type);
                Types[typeFullName].RegisterTypeDefinition(type.TypeFullName);
                return Types[typeFullName];
            }
        }

        /// <summary>
        /// Determines whether this instance can register the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// <c>true</c> if this instance can register the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRegister(Type type) => Filter.CanRegister(type);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="typeFullName">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(string typeFullName, object[] args)
        {
            if (Types.ContainsKey(typeFullName))
                return Types[typeFullName].CreateInstance(args);
            var runtimeType = TypeInvariants.ContainsKey(typeFullName) ? TypeInvariants[typeFullName] : null;
            var runtimeTypes = GetRuntimeTypes(Parser, typeFullName, args);
            runtimeType = runtimeTypes.Length == 1 ? runtimeTypes[0] : null;
            if (runtimeType != null)
                return runtimeType.CreateInstance(args);
            if (runtimeTypes.Length > 1)
            {
                if (args != null && args.Length == 0)
                {
                    runtimeType = runtimeTypes.FirstOrDefault((p) => p.Count == 0);
                    if (runtimeType != null)
                        return runtimeType.CreateInstance(args);
                }
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (more than one constructor available)", typeFullName));
            }
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", typeFullName));
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="typeFullName">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object GetInstance(string typeFullName, object[] args)
        {
            if (Types.ContainsKey(typeFullName))
                return Types[typeFullName].CreateInstance(args);
            var runtimeType = TypeInvariants.ContainsKey(typeFullName) ? TypeInvariants[typeFullName] : null;
            var runtimeTypes = GetRuntimeTypes(Parser, typeFullName, args);
            runtimeType = runtimeTypes.Length == 1 ? runtimeTypes[0] : null;
            if (runtimeType != null)
            {
                if (runtimeType.GetInstance)
                    return runtimeType.CreateInstance();
                return runtimeType.CreateInstance(GetRuntimeTypeParameters(runtimeType, args));
            }
            if (runtimeTypes.Length > 1)
            {
                throw new TypeInstantiationException(string.Format("{0} is not instantiated (more than one constructor available)", typeFullName));
            }
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", typeFullName));
        }

        /// <summary>
        /// Locks the container. Pre-computes all registered type invariants for lookup table speed up
        /// </summary>
        public void Lock()
        {
            if (!Locked)
            {
                Locked = true;
                foreach (var runtimeType in Types.Values)
                {
                    if (runtimeType.RuntimeTypes.Any())
                    {
                        foreach (var invariant in GetRuntimeTypes(runtimeType))
                        {
                            TypeInvariants.Add(invariant, runtimeType);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterType(Type type, object[] args)
        {
            if (!Locked)
            {
                Visited.Add(type);
                try
                {
                    RegisterConstructor(type);
                    if (args == null || args.Length == 0)
                        return;
                    RegisterConstructorParameters(type.ToString(), args);
                }
                catch (TypeRegistrationException ex)
                {
                    throw new TypeRegistrationException(string.Format("{0} is not registered", type), ex);
                }
                finally
                {
                    Visited.Remove(type);
                }
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            TypeInvariants.Clear();
            Types.Clear();
            Parser.Flush();
            Locked = false;
        }

        /// <summary>
        /// Unlocks the container. Flushes all pre-computed registered type invariants for lookup table speed up
        /// </summary>
        public void Unlock()
        {
            if (Locked)
            {
                Locked = false;
            }
        }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="typeFullName">The type.</param>
        /// <param name="parameterTypeFullName">Type of the parameter.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        static void CheckParametersFullName(string typeFullName, string parameterTypeFullName)
        {
            if (typeFullName == parameterTypeFullName)
                throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", typeFullName));
        }

        /// <summary>
        /// Gets the full name of the parameters.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="attributeType">Type of the attribute.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        void CheckTypeFullName(Type parameterType, Type attributeType)
        {
            if (attributeType != null && !Filter.CheckTypeFullName(parameterType, attributeType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.Name, attributeType.Name));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="dependencyObject">The type definition.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        void CheckTypeFullName(ITypeDependencyObject dependencyObject)
        {
            var constructor = dependencyObject.RuntimeType;
            string typeFullName = dependencyObject.TypeFullName;
            var attributeType = Resolver.GetType(constructor.ActivatorType.Assembly, typeFullName);
            if (attributeType != null && !Filter.CheckTypeFullName(attributeType, dependencyObject.RuntimeType.ActivatorType))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.Name, dependencyObject.RuntimeType.TypeFullName));
        }

        IEnumerable<string[]> GetRuntimeTypeInvariants(IRuntimeType runtimeType, IRuntimeType head, IEnumerable<IRuntimeType> tail)
        {
            foreach (var h in head.Types)
            {
                if (tail.Any())
                {
                    foreach (var t in GetRuntimeTypeInvariants(runtimeType, tail.First(), tail.Skip(1)))
                    {
                        var result = new List<string> { h };
                        result.AddRange(t);
                        yield return result.ToArray();
                    }
                }
                yield return new List<string> { h }.ToArray();
            }
        }

        object[] GetRuntimeTypeParameters(IRuntimeType runtimeType, object[] args)
        {
            var parameters = new List<object>();
            foreach (var parameter in runtimeType.RuntimeTypes)
            {
                var parameterRuntimeTypes = Types.Values.FindRuntimeTypes(Parser, parameter, args);
                if (parameterRuntimeTypes.Length == 1)
                {
                    var parameterRuntimeType = parameterRuntimeTypes[0];
                    if (parameterRuntimeType.GetInstance)
                        parameters.Add(parameterRuntimeType.CreateInstance());
                    else
                        parameters.Add(parameterRuntimeType.Value);
                }
                else
                {
                    parameters.Add(null);
                }
            }

            return parameters.ToArray();
        }

        IRuntimeType[] GetRuntimeTypes(ITypeParser typeParser, string typeFullName, object[] args) =>
            Types.Values.GetRuntimeTypes(typeParser, typeFullName, args);

        IEnumerable<string> GetRuntimeTypes(IRuntimeType runtimeType)
        {
            var length = runtimeType.RuntimeTypes.Count();
            foreach (var invariant in GetRuntimeTypeInvariants(runtimeType, runtimeType.RuntimeTypes.First(), runtimeType.RuntimeTypes.Skip(1)).Where((p) => p.Length == length))
                yield return Format.GetConstructorWithParameters(runtimeType.TypeFullName, invariant);
        }

        /// <summary>
        /// Registers the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructor(Type type)
        {
            if (!type.IsValueType)
            {
                var constructorEnumerator = Constructor.GetDependencyObjects(Activator, type, UseDefaultTypeInstantiation).GetEnumerator();
                if (!constructorEnumerator.MoveNext())
                    throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type));
                do
                {
                    var dependencyObject = constructorEnumerator.Current;
                    foreach (var injectionObject in dependencyObject.InjectionObjects)
                    {
                        RegisterConstructorParameter(dependencyObject, injectionObject);
                    }
                    RegisterConstructorDependencyObject(dependencyObject);
                } while (constructorEnumerator.MoveNext());
            }
        }

        /// <summary>
        /// Registers the type of the constructor.
        /// </summary>
        /// <param name="dependencyObject">Type dependency object.</param>
        void RegisterConstructorDependencyObject(ITypeDependencyObject dependencyObject)
        {
            var constructor = dependencyObject.RuntimeType;
            var constructorAttribute = dependencyObject.DependencyAttribute;
            CheckTypeFullName(dependencyObject);
            var typeFullName = dependencyObject.TypeFullNameWithParameters;
            if (!Types.ContainsKey(typeFullName))
            {
                var result = this[typeFullName, constructor];
                if (result != null)
                {
                    result.SetRuntimeInstance(constructorAttribute.RuntimeInstance);
                }
            }
        }

        /// <summary>
        /// Registers the constructor parameter.
        /// </summary>
        /// <param name="dependencyObject">The constructor.</param>
        /// <param name="injectionObject">The constructor parameter.</param>
        void RegisterConstructorParameter(ITypeDependencyObject dependencyObject, ITypeInjectionObject injectionObject)
        {
            var constructor = dependencyObject.RuntimeType;
            var constructorType = constructor.ActivatorType;
            var parameter = injectionObject.RuntimeType;
            var parameterType = parameter.ActivatorType;
            string typeFullName = injectionObject.TypeFullName;
            var attributeType = Resolver.GetType(constructorType.Assembly, typeFullName);
            CheckTypeFullName(parameterType, attributeType);
            CheckParametersFullName(constructorType.Name, parameterType.Name);
            var parameters = injectionObject.TypeParameters;
            var runtimeType = Types.Values.Count > 0 ? Parser.Find(typeFullName, parameters, Types.Values.ToArray()) : null;
            if (runtimeType != null && runtimeType.ActivatorType == parameter.ActivatorType)
                injectionObject.SetRuntimeType(runtimeType);
            if (UseDefaultTypeResolution && runtimeType == null)
                RegisterConstructorParameter(attributeType);
            RegisterConstructorType(parameterType);
            RegisterRuntimeType(dependencyObject, injectionObject);
        }

        /// <summary>
        /// Registers the parameter type of the constructor.
        /// </summary>
        /// <param name="type">The parameter type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructorParameter(Type type)
        {
            if (Filter.CanRegisterParameter(type))
            {
                if (Visited.Contains(type))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type));
                RegisterType(type, null);
            }
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="typeFullName">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        void RegisterConstructorParameters(string typeFullName, object[] args)
        {
            var parameterArgs = Format.GetParametersFullName(args);
            var runtimeType = Parser.FindRuntimeTypes(typeFullName, parameterArgs, Types.Values).FirstOrDefault();
            if (runtimeType == null)
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", typeFullName));
            runtimeType.SetRuntimeInstance(RuntimeInstance.GetInstance);
            runtimeType.RegisterConstructorParameters(args);
        }

        /// <summary>
        /// Registers the type of the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructorType(Type type)
        {
            if (CanRegister(type))
            {
                if (Visited.Contains(type))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type));
                RegisterType(type, null);
            }
        }

        void RegisterRuntimeType(ITypeDependencyObject dependencyObject, ITypeInjectionObject injectionObject)
        {
            var constructor = dependencyObject.RuntimeType;
            var parameter = injectionObject.RuntimeType;
            var typeFullName = injectionObject.TypeFullNameWithParameters;
            var result = this[typeFullName, parameter];
            if (result != null)
            {
                var constructorFullName = dependencyObject.TypeFullNameWithParameters;
                result.Attribute.RegisterReferenceAttrubute(constructorFullName, injectionObject.InjectionAttribute, UseDefaultTypeAttributeOverwrite);
                constructor.AddConstructorParameter(CanRegister(result.ActivatorType), result);
            }
        }
    }

    /// <summary>
    /// Optional type builder parameters
    /// </summary>
    public sealed class TypeBuilderOptions
    {
        /// <summary>
        /// Creates an instance of the specified runtime type
        /// </summary>
        public ITypeActivator Activator { get; set; } = new TypeActivator();

        /// <summary>
        /// Constructs type dependency
        /// </summary>
        public ITypeConstructor Constructor { get; set; } = new TypeConstructor();

        /// <summary>
        /// Gets type filter
        /// </summary>
        /// <value>The filter</value>
        public ITypeFilter Filter { get; set; } = new TypeFilter();

        /// <summary>
        /// Gets type parser
        /// </summary>
        /// <value>The parser</value>
        public ITypeParser Parser { get; set; } = new TypeParser();

        /// <summary>
        /// Gets type resolver
        /// </summary>
        /// <value>The resolver</value>
        public ITypeResolver Resolver { get; set; } = new TypeResolver();

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        public bool? UseDefaultTypeAttributeOverwrite { get; set; }

        /// <summary>
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        public bool? UseDefaultTypeInstantiation { get; set; }

        /// <summary>
        /// True if automatic type resolution for reference types option enabled (does not throws
        /// exceptions for reference types contains type dependencies to non-registered types)
        /// </summary>
        /// <remarks>
        /// If automatic type resolution for reference types is enabled, type will defaults to null
        /// if not resolved and no exception will be thrown
        /// </remarks>
        public bool? UseDefaultTypeResolution { get; set; }
    }
}