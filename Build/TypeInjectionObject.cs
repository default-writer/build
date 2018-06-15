using System.Reflection;

namespace Build
{
    class TypeInjectionObject : ITypeInjectionObject
    {
        public TypeInjectionObject(ParameterInfo parameterInfo, bool defaultTypeInstantiation)
        {
            InjectionAttribute = GetInjectionAttribute(parameterInfo);
            RuntimeType = new RuntimeType(InjectionAttribute, parameterInfo.ParameterType, defaultTypeInstantiation);
        }

        public IInjectionAttribute InjectionAttribute { get; }

        public IRuntimeType RuntimeType { get; }

        /// <summary>
        /// Gets the injection attribute. (ParameterInfo's ParameterType)
        /// </summary>
        /// <param name="parameterInfo">The parameter.</param>
        /// <returns></returns>
        static InjectionAttribute GetInjectionAttribute(ParameterInfo parameterInfo) => parameterInfo.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameterInfo.ParameterType);
    }
}