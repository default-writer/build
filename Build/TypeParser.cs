using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

namespace Build
{
    /// <summary>
    /// Type parser
    /// </summary>
    /// <seealso cref="Build.ITypeParser"/>
    public sealed class TypeParser : ITypeParser
    {
        /// <summary>
        /// Cache for RuntimeTtype.
        /// </summary>
        /// <value>The cache.</value>
        IDictionary<string, IRuntimeType> Cache { get; } = new Dictionary<string, IRuntimeType>();

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public IRuntimeType Find(string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types)
        {
            if (Cache.ContainsKey(id))
                return Cache[id];
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = Regex.Matches(func.Groups[2].Value.Trim(), @"([^,]+\(.+?\))|([^,]+)");
            var runtimeType = types.FirstOrDefault((p) => MatchParameters(p, name, args, pars));
            return CacheRuntimeType(id, runtimeType);
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public IEnumerable<IRuntimeType> FindAll(string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types)
        {
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = Regex.Matches(func.Groups[2].Value.Trim(), @"([^,]+\(.+?\))|([^,]+)");
            return types.Where((p) => MatchParameters(p, name, args, pars));
        }

        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        static bool MatchArguments(IEnumerable<string> arguments, IEnumerable<IRuntimeType> parameters)
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
        /// Matches the parameter arguments.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        static bool MatchParameterArguments(IRuntimeType runtimeType, IEnumerable<string> args)
        {
            var count = args.Count();
            if (count > 0 && runtimeType.Count != count)
                return false;
            if (!MatchArguments(args, runtimeType.RuntimeTypes))
                return false;
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
        static bool MatchParameters(IRuntimeType runtimeType, string name, IEnumerable<string> args, MatchCollection match)
        {
            if (!MatchType(runtimeType, name))
                return false;
            return MatchParameters(runtimeType, match) && MatchParameterArguments(runtimeType, args);
        }

        /// <summary>
        /// Matches the parameters.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        static bool MatchParameters(IRuntimeType runtimeType, MatchCollection match)
        {
            if (match.Count > 0 && runtimeType.Count != match.Count)
                return false;
            if (!MatchArguments(match.Cast<Match>().Select(capture => capture.Value.Trim()), runtimeType.RuntimeTypes))
                return false;
            return true;
        }

        /// <summary>
        /// Matches the type.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        static bool MatchType(IRuntimeType runtimeType, string name) => runtimeType.TypeFullName == name;

        /// <summary>
        /// Caches the type of the runtime.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <returns></returns>
        IRuntimeType CacheRuntimeType(string id, IRuntimeType runtimeType)
        {
            if (runtimeType != null)
            {
                if (!Cache.ContainsKey(id))
                    Cache.Add(id, runtimeType);
                return Cache[id];
            }
            return runtimeType;
        }
    }
}