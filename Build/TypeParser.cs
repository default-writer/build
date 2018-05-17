using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

namespace Build
{
    /// <summary>
    /// Type parser
    /// </summary>
    /// <seealso cref="Build.ITypeParser"/>
    class TypeParser : ITypeParser
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
        public IRuntimeType Find(string id, string[] args, IEnumerable<IRuntimeType> types)
        {
            if (Cache.ContainsKey(id))
                return Cache[id];
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = Regex.Matches(func.Groups[2].Value.Trim(), @"([^,]+\(.+?\))|([^,]+)");
            var runtimeType = GetRuntimeType(args, types, name, pars);
            return CacheRuntimeType(id, runtimeType);
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
                if (!parameterType.IsAssignableFrom(argumentType))
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
        static bool MatchParameterArguments(IRuntimeType runtimeType, string[] args)
        {
            if (args.Length > 0 && runtimeType.ParametersCount != args.Length)
                return false;
            if (!MatchArguments(args, runtimeType.RuntimeParameters))
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
        static bool MatchParameters(IRuntimeType runtimeType, string name, string[] args, MatchCollection match)
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
            if (match.Count > 0 && runtimeType.ParametersCount != match.Count)
                return false;
            if (!MatchArguments(match.Select(capture => capture.Value.Trim()), runtimeType.RuntimeParameters))
                return false;
            return true;
        }

        /// <summary>
        /// Matches the type.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        static bool MatchType(IRuntimeType runtimeType, string name) => runtimeType.Type.FullName == name;

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

        /// <summary>
        /// Gets the type of the runtime.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <param name="name">The name.</param>
        /// <param name="pars">The pars.</param>
        /// <returns></returns>
        IRuntimeType GetRuntimeType(string[] args, IEnumerable<IRuntimeType> types, string name, MatchCollection pars)
        {
            var runtimeType = types.FirstOrDefault((p) => MatchParameters(p, name, args, pars));
            if (runtimeType == null)
            {
                var enumerator = types.GetEnumerator();
                while (runtimeType == null && enumerator.MoveNext())
                {
                    var parameterType = enumerator.Current;
                    runtimeType = Find(parameterType.Id, args, parameterType.RuntimeParameters);
                }
            }

            return runtimeType;
        }
    }
}