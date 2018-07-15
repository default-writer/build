using System;

namespace Build
{
    public sealed class TypeInjectionObject : TypeObject, ITypeInjectionObject
    {
        public TypeInjectionObject(ITypeActivator runtimeTypeActivator, IInjectionAttribute injectionAttribute, Type runtimeType, bool defaultTypeInstantiation) : base(runtimeTypeActivator, injectionAttribute, runtimeType, injectionAttribute.Arguments, defaultTypeInstantiation) => InjectionAttribute = injectionAttribute;

        /// <summary>
        /// Injection attribute
        /// </summary>
        public IInjectionAttribute InjectionAttribute { get; }
    }
}