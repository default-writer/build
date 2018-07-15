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
        IDictionary<IRuntimeType, string> Cache { get; } = new Dictionary<IRuntimeType, string>();

        /// <summary>
        /// Clears all precomputed registered type invariants
        /// </summary>
        public void Clear() => Cache.Clear();

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="typeId">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public IEnumerable<IRuntimeType> FindRuntimeTypes(string typeId, IEnumerable<string> args, IEnumerable<IRuntimeType> types)
        {
            var id = Regex.Replace(typeId, @"\s", "");
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var constructor = id == func.Groups[1].Value ? Format.GetConstructor(id, args) : id;
            var cached = Cache.Where((p) => p.Value == constructor).Select((p) => p.Key);
            var cachedCount = cached.Count();
            if (cachedCount > 0)
                return cached;
            var count = types.Count();
            if (count > 0)
            {
                var name = func.Groups[1].Value;
                var pars = Regex.Matches(func.Groups[2].Value, @"([^,]+\(.+?\))|([^,]+)");
                IRuntimeType CacheRuntimeType(IRuntimeType runtimeType)
                {
                    if (!Cache.ContainsKey(runtimeType))
                        Cache.Add(runtimeType, constructor);
                    return runtimeType;
                }
                return types.Where((p) => p.MatchParameters(name, args, pars)).Select(CacheRuntimeType);
            }
            return new IRuntimeType[0];
        }
    }
}