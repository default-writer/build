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
        /// <param name="defaultTypeResolution">
        /// Parameter defaults to true for automatic type resolution enabled. If value is false and
        /// not all type dependencies are resolved, exception will be thrown
        /// </param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        public TypeBuilder(bool defaultTypeResolution, bool defaultTypeInstantiation, bool defaultTypeAttributeOverwrite)
        {
            UseDefaultTypeResolution = defaultTypeResolution;
            UseDefaultTypeInstantiation = defaultTypeInstantiation;
            UseDefaultTypeAttributeOverwrite = defaultTypeAttributeOverwrite;
            Constructor = new TypeConstructor();
            Filter = new TypeFilter();
            Resolver = new TypeResolver();
            Parser = new TypeParser();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeBuilder"/> class.
        /// </summary>
        /// <param name="typeConstructor">Type constructor</param>
        /// <param name="typeFilter">Type filter</param>
        /// <param name="typeParser">Type parser</param>
        /// <param name="typeResolver">Type resolver</param>
        /// <param name="defaultTypeResolution">
        /// Parameter defaults to true for automatic type resolution enabled. If value is false and
        /// not all type dependencies are resolved, exception will be thrown
        /// </param>
        /// <param name="defaultTypeInstantiation">
        /// Parameter defaults to true for automatic type instantiation enabled. If value is false
        /// and type is resolved to default value for reference type, exception will be thrown
        /// </param>
        /// <param name="defaultTypeAttributeOverwrite">
        /// Parameter defaults to true for automatic type attribute overwrite. If value is false
        /// exception will be thrown for type attribute overwrites
        /// </param>
        public TypeBuilder(ITypeConstructor typeConstructor, ITypeFilter typeFilter, ITypeParser typeParser, ITypeResolver typeResolver, bool defaultTypeResolution, bool defaultTypeInstantiation, bool defaultTypeAttributeOverwrite)
        {
            UseDefaultTypeResolution = defaultTypeResolution;
            UseDefaultTypeInstantiation = defaultTypeInstantiation;
            UseDefaultTypeAttributeOverwrite = defaultTypeAttributeOverwrite;
            Constructor = typeConstructor ?? throw new ArgumentNullException(nameof(typeConstructor));
            Filter = typeFilter ?? throw new ArgumentNullException(nameof(typeFilter));
            Resolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            Parser = typeParser ?? throw new ArgumentNullException(nameof(typeParser));
        }

        /// <summary>
        /// Constructs type dependency
        /// </summary>
        public ITypeConstructor Constructor { get; }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public ITypeFilter Filter { get; }

        /// <summary>
        /// Gets the parser.
        /// </summary>
        /// <value>The parser.</value>
        public ITypeParser Parser { get; }

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
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
        /// True if automatic type instantiation for reference types option enabled (does not throws
        /// exceptions for reference types defaults to null)
        /// </summary>
        /// <remarks>
        /// If automatic type instantiation for reference types is enabled, type will defaults to
        /// null if not resolved and no exception will be thrown
        /// </remarks>
        public bool UseDefaultTypeAttributeOverwrite { get; }

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
        /// True if automatic type resolution for reference types option enabled (does not throws
        /// exceptions for reference types contains type dependencies to non-registered types)
        /// </summary>
        /// <remarks>
        /// If automatic type resolution for reference types is enabled, type will defaults to null
        /// if not resolved and no exception will be thrown
        /// </remarks>
        public bool UseDefaultTypeResolution { get; }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <value>The types.</value>
        IDictionary<string, IRuntimeType> Types { get; } = new Dictionary<string, IRuntimeType>();

        /// <summary>
        /// Gets the visited.
        /// </summary>
        /// <value>The visited.</value>
        List<Type> Visited { get; } = new List<Type>();

        /// <summary>
        /// Gets the <see cref="RuntimeType"/> with the specified identifier.
        /// </summary>
        /// <value>The <see cref="RuntimeType"/>.</value>
        /// <param name="id">The identifier.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IRuntimeType this[string id, IRuntimeType type]
        {
            get
            {
                if (!Types.ContainsKey(id))
                    Types.Add(id, type);
                Types[id].RegisterTypeDefinition(type.TypeFullName);
                return Types[id];
            }
        }

        /// <summary>
        /// Determines whether this instance can create the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can create the specified type; otherwise, <c>false</c>.</returns>
        public bool CanCreate(Type type) => Filter.CanCreate(type);

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
        /// <param name="type">The type.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public object CreateInstance(Type type, params object[] args) => CreateInstance(type.FullName, args);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="TypeInstantiationException"></exception>
        public object CreateInstance(string id, params object[] args)
        {
            if (Types.ContainsKey(id))
                return Types[id].CreateInstance(args);
            var parameterArgs = Format.GetParametersFullName(args);
            var runtimeType = Parser.Find(id, parameterArgs, Types.Values);
            if (runtimeType != null) return runtimeType.CreateInstance(args);
            throw new TypeInstantiationException(string.Format("{0} is not instantiated (no constructors available)", id));
        }

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RegisterType(Type type)
        {
            Visited.Add(type);
            try
            {
                RegisterConstructor(type);
            }
            catch (TypeRegistrationException ex)
            {
                throw new TypeRegistrationException(string.Format("{0} is not registered", type.FullName), ex);
            }
            finally
            {
                Visited.Remove(type);
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        internal void Reset() => Types.Clear();

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
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", parameterType.FullName, attributeType.FullName));
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="dependencyObject">The type definition.</param>
        /// <returns></returns>
        /// <exception cref="TypeRegistrationException"></exception>
        void CheckTypeFullName(ITypeDependencyObject dependencyObject)
        {
            var attributeType = Resolver.GetType(dependencyObject.RuntimeType.Type.Assembly, dependencyObject.TypeFullName);
            if (attributeType != null && !Filter.CheckTypeFullName(attributeType, dependencyObject.RuntimeType.Type))
                throw new TypeRegistrationException(string.Format("{0} is not registered (not assignable from {1})", attributeType.FullName, dependencyObject.RuntimeType.TypeFullName));
        }

        /// <summary>
        /// Registers the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructor(Type type)
        {
            var constructorEnumerator = Constructor.GetDependencyObjects(type, UseDefaultTypeInstantiation).GetEnumerator();
            if (!constructorEnumerator.MoveNext())
                throw new TypeRegistrationException(string.Format("{0} is not registered (no constructors available)", type.FullName));
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
            var constructorType = constructor.Type;
            var parameter = injectionObject.RuntimeType;
            var parameterType = parameter.Type;
            string typeFullName = injectionObject.TypeFullName;
            var attributeType = Resolver.GetType(constructorType.Assembly, typeFullName);
            CheckTypeFullName(parameterType, attributeType);
            CheckParametersFullName(constructorType.FullName, parameterType.FullName);
            var parameters = injectionObject.TypeParameters;
            var runtimeType = Parser.Find(typeFullName, parameters, Types.Values);
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
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                RegisterType(type);
            }
        }

        /// <summary>
        /// Registers the type of the constructor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="TypeRegistrationException"></exception>
        void RegisterConstructorType(Type type)
        {
            if (Filter.CanRegister(type))
            {
                if (Visited.Contains(type))
                    throw new TypeRegistrationException(string.Format("{0} is not registered (circular references found)", type.FullName));
                RegisterType(type);
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
                result.Attribute.RegisterRuntimeType(constructorFullName, injectionObject.InjectionAttribute, UseDefaultTypeAttributeOverwrite);
                constructor.AddParameter(result);
            }
        }
    }
}