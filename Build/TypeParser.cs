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
        HashSet<IRuntimeType> Cache { get; } = new HashSet<IRuntimeType>();

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public IEnumerable<IRuntimeType> FindRuntimeTypes(string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types)
        {
            var runtimeTypes = Cache.Where((p) => id == p.TypeFullName);
            var count = runtimeTypes.Count();
            if (count > 0)
                return runtimeTypes;
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = Regex.Matches(func.Groups[2].Value.Trim(), @"([^,]+\(.+?\))|([^,]+)");
            return types.Where((p) => MatchParameters(p, name, args, pars)).Select(Index);
        }

        /// <summary>
        /// Matches the arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        static bool Match(IEnumerable<string> arguments, IEnumerable<IRuntimeType> parameters)
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
        static bool MatchParameters(IRuntimeType runtimeType, string name, IEnumerable<string> args, MatchCollection match)
            => MatchType(runtimeType, name)
            && MatchRuntimeTypes(runtimeType, match.Cast<Match>().Select((p) => p.Value.Trim()))
            && MatchRuntimeTypes(runtimeType, args);

        /// <summary>
        /// Matches the parameter arguments.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        static bool MatchRuntimeTypes(IRuntimeType runtimeType, IEnumerable<string> args)
        {
            var count = args.Count();
            return count == 0 || (runtimeType.Count == count && Match(args, runtimeType.RuntimeTypes));
        }

        /// <summary>
        /// Matches the type.
        /// </summary>
        /// <param name="runtimeType">Type of the runtime.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        static bool MatchType(IRuntimeType runtimeType, string name) => runtimeType.TypeFullName == name;

        IRuntimeType Index(IRuntimeType runtimeType)
        {
            Cache.Add(runtimeType);
            return runtimeType;
        }
    }
}