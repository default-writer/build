using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Build
{
    public static class RuntimeTypeExtensions
    {
        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static bool ContainsTypeDefinition(this IEnumerable<IRuntimeType> parameters, IEnumerable<string> arguments)
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
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static IRuntimeType[] FindRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, string typeId) =>
            runtimeTypes.Where((p) => typeId == Format.GetConstructor(p.TypeFullName, p.RuntimeTypes.Select((s) => s.TypeFullName))).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <param name="runtimeTypes">Types</param>
        /// <param name="typeParser">Parset</param>
        /// <param name="runtimeType">Id</param>
        /// <param name="args">Values</param>
        /// <returns></returns>
        public static IRuntimeType[] FindRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, IRuntimeType runtimeType, object[] args) =>
            typeParser.FindRuntimeTypes(runtimeType.TypeFullName, Format.GetNames(args), runtimeTypes.Where((p) => p.Attribute is IDependencyAttribute)).Where((p) => p.ActivatorType == runtimeType.ActivatorType).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <param name="runtimeTypes">Types</param>
        /// <param name="typeParser">Parset</param>
        /// <param name="typeId">Id</param>
        /// <param name="args">Values</param>
        /// <returns></returns>
        public static IRuntimeType[] GetRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, string typeId, object[] args) =>
            typeParser.FindRuntimeTypes(typeId, Format.GetNames(args), runtimeTypes.Where((p) => p.Attribute is IDependencyAttribute)).ToArray();

        /// <summary>
        /// Finds all dependency runtime types (instantiable types) which matches the criteria
        /// </summary>
        /// <param name="runtimeTypes">Types</param>
        /// <param name="typeParser">Parset</param>
        /// <param name="typeId">Id</param>
        /// <param name="args">Values</param>
        /// <returns></returns>
        public static IRuntimeType[] GetRuntimeTypes(this IEnumerable<IRuntimeType> runtimeTypes, ITypeParser typeParser, string typeId, string[] args) =>
            typeParser.FindRuntimeTypes(typeId, Format.GetNames(args), runtimeTypes.Where((p) => p.Attribute is IDependencyAttribute)).ToArray();

        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static bool IsAssignableFrom(this IEnumerable<IRuntimeType> parameters, IEnumerable<Type> arguments)
        {
            var args = arguments.GetEnumerator();
            var pars = parameters.GetEnumerator();
            while (args.MoveNext() && pars.MoveNext())
            {
                var argumentType = args.Current;
                var parameterType = pars.Current;
                if (argumentType != null && !parameterType.ActivatorType.IsAssignableFrom(argumentType))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static bool IsOfType(this IEnumerable<IRuntimeType> parameters, IEnumerable<Type> arguments)
        {
            var args = arguments.GetEnumerator();
            var pars = parameters.GetEnumerator();
            while (args.MoveNext() && pars.MoveNext())
            {
                var argumentType = args.Current;
                var parameterType = pars.Current;
                if (parameterType.ActivatorType != argumentType)
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
            && MatchRuntimeTypes(runtimeType, match.Cast<Match>().Select((p) => p.Value))
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
            return count == 0 || (runtimeType.Count == count && ContainsTypeDefinition(runtimeType.RuntimeTypes, args));
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
        /// <param name="runtimeTypes">Types</param>
        /// <param name="runtimeAttribute">Attribute</param>
        /// <param name="typeId">Id</param>
        /// <returns>Returns runtime types values</returns>
        public static object[] Values(this IEnumerable<IRuntimeType> runtimeTypes, IRuntimeAttribute runtimeAttribute, string typeId) =>
            runtimeTypes.Select((p) => p.GetValue(runtimeAttribute, typeId)).ToArray();
    }
}