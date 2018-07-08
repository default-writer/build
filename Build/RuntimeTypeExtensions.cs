using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        /// Finds all dependency runtime types with full parameters specified
        /// </summary>
        /// <param name="runtimeTypes"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IRuntimeType[] FindRintimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, string id) =>
            runtimeTypes.Where((p) => id == Format.GetConstructorWithParameters(p.TypeFullName, p.RuntimeTypes.Select((s) => s.TypeFullName))).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <returns></returns>
        public static IRuntimeType[] FindRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, IRuntimeType runtimeType, object[] args = null) =>
            typeParser.FindRuntimeTypes(runtimeType.TypeFullName, Format.GetParametersFullName(args), runtimeTypes.Where((p) => p.Attribute is IDependencyAttribute)).Where((p) => p.Type == runtimeType.Type).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <returns></returns>
        public static IRuntimeType[] GetRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, string typeFullName, object[] args = null) =>
            typeParser.FindRuntimeTypes(typeFullName, Format.GetParametersFullName(args), runtimeTypes.Where((p) => p.Attribute is IDependencyAttribute)).ToArray();

        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static bool Match(this IEnumerable<IRuntimeType> parameters, IEnumerable<string> arguments)
        {
            var args = arguments.GetEnumerator();
            var pars = parameters.GetEnumerator();
            while (args.MoveNext() && pars.MoveNext())
            {
                var argumentType = args.Current;
                var parameterType = pars.Current;
                if (!parameterType.ContainsTypeDefinition(argumentType))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Matches the parameters.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="name">The name.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static bool MatchParameters(this IRuntimeType runtimeType, string name, IEnumerable<string> args, MatchCollection match)
            => MatchType(runtimeType, name)
            && MatchRuntimeTypes(runtimeType, match.Cast<Match>().Select((p) => p.Value.Trim()))
            && MatchRuntimeTypes(runtimeType, args);

        /// <summary>
        /// Matches the parameter arguments.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static bool MatchRuntimeTypes(this IRuntimeType runtimeType, IEnumerable<string> args)
        {
            var count = args.Count();
            return count == 0 || (runtimeType.Count == count && Match(runtimeType.RuntimeTypes, args));
        }

        /// <summary>
        /// Matches the type.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static bool MatchType(this IRuntimeType runtimeType, string name) => runtimeType.TypeFullName == name;

        /// <summary>
        /// Gets runtime type values
        /// </summary>
        /// <param name="runtimeTypes"></param>
        /// <returns>Returns runtime types values</returns>
        public static object[] Values(this IEnumerable<IRuntimeType> runtimeTypes, IRuntimeAttribute runtimeAttribute, string id) =>
            runtimeTypes.Select((p) => p.GetValue(runtimeAttribute, id)).ToArray();
    }
}