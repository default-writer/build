using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Build.Tests.Classes
{
    public sealed class InterfaceTypeParser : ITypeParser
    {
        /// <summary>
        /// Cache for RuntimeTtype.
        /// </summary>
        /// <value>The cache.</value>
        IDictionary<IRuntimeType, string> Cache { get; } = new Dictionary<IRuntimeType, string>();

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public IEnumerable<IRuntimeType> FindRuntimeTypes(string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types)
        {
            id = Regex.Replace(id, @"\s", "");
            IRuntimeType CacheRuntimeType(IRuntimeType runtimeType)
            {
                if (!Cache.ContainsKey(runtimeType))
                    Cache.Add(runtimeType, id);
                Cache[runtimeType] = id;
                return runtimeType;
            }
            var cached = Cache.Where((p) => p.Value == id).Select((p) => p.Key);
            var cachedCount = cached.Count();
            if (cachedCount > 0)
                return cached;
            var count = types.Count();
            if (count > 0)
            {
                var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
                var name = func.Groups[1].Value;
                var pars = Regex.Matches(func.Groups[2].Value, @"([^,]+\(.+?\))|([^,]+)");
                return types.Where((p) => p.MatchParameters(name, args, pars)).Select(CacheRuntimeType);
            }
            return new IRuntimeType[0];
        }

        /// <summary>
        /// Flushes all pre - computed registered type invariants
        /// </summary>
        public void Flush() => Cache.Clear();
    }
}