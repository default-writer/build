namespace Build
{
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
        /// True if default constructor is selected for emply argument list, does not affect
        /// types with single constructor defined which by convertion is a default constructor
        /// </summary>
        /// <remarks>Defaults to true</remarks>
        public bool? UseDefaultConstructor { get; set; }

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

        /// <summary>
        /// Allows use of value types
        /// </summary>
        public bool? UseValueTypes { get; set; }
    }
}