using System;
using System.Collections.Generic;
using System.Reflection;

namespace Build
{
    public sealed class TypeInjectionObject : TypeObject, ITypeInjectionObject
    {
        public TypeInjectionObject(IInjectionAttribute runtimeAttribute, Type runtimeType, bool defaultTypeInstantiation) : base(GetInjectionAttribute(runtimeAttribute, runtimeType), runtimeType, defaultTypeInstantiation)
        {
            var injectionAttribute = (IInjectionAttribute)RuntimeAttribute;
            InjectionAttribute = injectionAttribute;
            TypeParameters = injectionAttribute.Arguments;
            TypeFullNameWithParameters = Format.GetConstructorWithParameters(TypeFullName, TypeParameters);
        }

        /// <summary>
        /// Injection attribute
        /// </summary>
        public IInjectionAttribute InjectionAttribute { get; }

        /// <summary>
        /// Type full name with parameters
        /// </summary>
        public string TypeFullNameWithParameters { get; }

        /// <summary>
        /// Type parameters full name
        /// </summary>
        public IEnumerable<string> TypeParameters { get; }

        /// <summary>
        /// Gets the injection attribute. (ParameterInfo's ParameterType)
        /// </summary>
        /// <param name="runtimeAttribute">The parameter.</param>
        /// <param name="runtimeType">The type to be in</param>
        /// <returns></returns>
        static IRuntimeAttribute GetInjectionAttribute(IRuntimeAttribute runtimeAttribute, Type runtimeType) => runtimeAttribute ?? new InjectionAttribute(runtimeType);
    }
}