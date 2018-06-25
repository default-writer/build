using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public static class RuntimeTypeExtensions
    {
        /// <summary>
        /// Evaluates arguments
        /// </summary>
        /// <param name="runtimeTypes">Runtime types</param>
        /// <param name="runtimeType">Runtime type</param>
        /// <param name="runtimeAttribute">Runtime attribute</param>
        /// <returns>Returns runtime types evalated values</returns>
        public static object[] Evaluate(this IEnumerable<IRuntimeType> runtimeTypes, IRuntimeType runtimeType, IRuntimeAttribute runtimeAttribute) =>
            runtimeTypes.Select((p, i) => p.Evaluate(runtimeType, runtimeAttribute, i)).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <returns></returns>
        public static IRuntimeType[] FilterRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, IRuntimeType runtimeType, params object[] args) =>
            typeParser.FindAll(runtimeType.TypeFullName, Format.GetParametersFullName(args), runtimeTypes.Where((p) => p.Attribute is DependencyAttribute)).Where((p) => p.Type == runtimeType.Type).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <returns></returns>
        public static IRuntimeType[] GetRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, string typeFullName, params object[] args) =>
            typeParser.FindAll(typeFullName, Format.GetParametersFullName(args), runtimeTypes.Where((p) => p.Attribute is DependencyAttribute)).ToArray();

        /// <summary>
        /// Gets runtime type values
        /// </summary>
        /// <param name="runtimeTypes"></param>
        /// <returns>Returns runtime types values</returns>
        public static object[] Values(this IEnumerable<IRuntimeType> runtimeTypes, IRuntimeAttribute runtimeAttribute, string id) =>
            runtimeTypes.Select((p) => p.GetValue(runtimeAttribute, id)).ToArray();
    }
}