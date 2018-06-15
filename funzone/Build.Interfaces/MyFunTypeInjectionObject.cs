using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Build.Interfaces
{
    class MyFunTypeInjectionObject : TypeObject, ITypeInjectionObject
    {
        public MyFunTypeInjectionObject(ParameterInfo parameterInfo, bool defaultTypeInstantiation) : base(GetInjectionAttribute(parameterInfo), parameterInfo.ParameterType, defaultTypeInstantiation)
        {
            var injectionAttribute = (InjectionAttribute)RuntimeAttribute;
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
        /// <param name="parameterInfo">The parameter.</param>
        /// <returns></returns>
        static InjectionAttribute GetInjectionAttribute(ParameterInfo parameterInfo) => parameterInfo.GetCustomAttribute<InjectionAttribute>() ?? new InjectionAttribute(parameterInfo.ParameterType);
    }
}