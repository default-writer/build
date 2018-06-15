using System.Collections.Generic;
using System.Reflection;

namespace Build
{
    class TypeInjectionObject : TypeObject, ITypeInjectionObject
    {
        public TypeInjectionObject(ParameterInfo parameterInfo, bool defaultTypeInstantiation) : base(GetInjectionAttribute(parameterInfo), parameterInfo.ParameterType, defaultTypeInstantiation)
        {
            var injectionAttribute = (InjectionAttribute)RuntimeAttribute;
            InjectionAttribute = injectionAttribute;
            InjectedTypes = injectionAttribute.Arguments;
        }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        public IEnumerable<string> InjectedTypes { get; }

        public IInjectionAttribute InjectionAttribute { get; }

        /// <summary>
        /// Gets the injection attribute. (ParameterInfo's ParameterType)
        /// </summary>
        /// <param name="parameterInfo">The parameter.</param>
        /// <returns></returns>
        static InjectionAttribute GetInjectionAttribute(ParameterInfo parameterInfo) => parameterInfo.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameterInfo.ParameterType);
    }
}