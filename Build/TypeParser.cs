using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

namespace Build
{
    class TypeParser : ITypeParser
    {
        IDictionary<string, IRuntimeType> _cache = new Dictionary<string, IRuntimeType>();

        public IRuntimeType Find(string id, string[] args, IEnumerable<IRuntimeType> types)
        {
            if (_cache.ContainsKey(id))
                return _cache[id];
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
            if (runtimeType != null)
            {
                if (!_cache.ContainsKey(id))
                    _cache.Add(id, runtimeType);
                return _cache[id];
            }
            return runtimeType;
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

        static bool MatchArguments(IRuntimeType runtimeType, string[] args) => runtimeType.RuntimeParameters.Length == args.Length;

        static bool MatchParameters(IRuntimeType runtimeType, string name, string[] args, MatchCollection match)
        {
            if (!MatchType(runtimeType, name))
                return false;
            if (match.Count > 0 && !MatchParameters(runtimeType, match))
                return false;
            if (!Match(match.Select(capture => capture.Value.Trim()), runtimeType.RuntimeParameters))
                return false;
            if (args.Length > 0 && !MatchArguments(runtimeType, args))
                return false;
            if (!Match(args, runtimeType.RuntimeParameters))
                return false;
            return true;
        }

        static bool MatchParameters(IRuntimeType runtimeType, MatchCollection match) => runtimeType.RuntimeParameters.Length == match.Count;

        static bool MatchType(IRuntimeType runtimeType, string name) => runtimeType.Type.FullName == name;
    }
}