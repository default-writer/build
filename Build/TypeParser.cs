using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

namespace Build
{
    class TypeParser : ITypeParser
    {
        IDictionary<string, IRuntimeType> Cache { get; } = new Dictionary<string, IRuntimeType>();

        public IRuntimeType Find(string id, string[] args, IEnumerable<IRuntimeType> types)
        {
            if (Cache.ContainsKey(id))
                return Cache[id];
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = Regex.Matches(func.Groups[2].Value.Trim(), @"([^,]+\(.+?\))|([^,]+)");
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
            return CacheRuntimeType(id, runtimeType);
        }

        static bool Match(IEnumerable<string> arguments, IEnumerable<IRuntimeType> parameters)
        {
            var args = arguments.GetEnumerator();
            var pars = parameters.GetEnumerator();
            while (args.MoveNext() && pars.MoveNext())
            {
                var argumentType = args.Current;
                var parameterType = pars.Current;
                if (parameterType.Type.FullName != argumentType)
                    if (!parameterType.IsAssignableFrom(argumentType))
                        return false;
            }
            return true;
        }

        static bool MatchArguments(IRuntimeType runtimeType, string[] args)
        {
            if (args.Length > 0 && runtimeType.ParametersCount != args.Length)
                return false;
            if (!Match(args, runtimeType.RuntimeParameters))
                return false;
            return true;
        }

        static bool MatchParameters(IRuntimeType runtimeType, string name, string[] args, MatchCollection match)
        {
            if (!MatchType(runtimeType, name))
                return false;
            return MatchParameters(runtimeType, match) && MatchArguments(runtimeType, args);
        }

        static bool MatchParameters(IRuntimeType runtimeType, MatchCollection match)
        {
            if (match.Count > 0 && runtimeType.ParametersCount != match.Count)
                return false;
            if (!Match(match.Select(capture => capture.Value.Trim()), runtimeType.RuntimeParameters))
                return false;
            return true;
        }

        static bool MatchType(IRuntimeType runtimeType, string name) => runtimeType.Type.FullName == name;

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