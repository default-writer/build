using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;

namespace Build
{
    class TypeParser : ITypeParser
    {
        IDictionary<string, IRuntimeType> _cache = new Dictionary<string, IRuntimeType>();

        public IRuntimeType Find(string typeId, string[] args, IEnumerable<IRuntimeType> types)
        {
            if (_cache.ContainsKey(typeId))
                return _cache[typeId];
            var func = Regex.Match(typeId, @"([^()]+)(?:\((.*)\)){0,1}$");
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
                if (!_cache.ContainsKey(typeId))
                    _cache.Add(typeId, runtimeType);
                return _cache[typeId];
            }
            return runtimeType;
        }

        static bool MatchParameters(IRuntimeType runtimeType, string name, string[] args, MatchCollection match)
        {
            if (runtimeType.Type.FullName != name)
                return false;
            if (match.Count > 0 && runtimeType.RuntimeParameters.Length != match.Count)
                return false;
            for (int i = 0; i < match.Count; i++)
            {
                var argumentType = match[i].Value.Trim();
                var parameterType = runtimeType.RuntimeParameters[i];
                if (parameterType.Type.FullName != argumentType)
                    if (!parameterType.IsAssignableFrom(argumentType))
                        return false;
            }
            if (args.Length != 0 && args.Length != runtimeType.RuntimeParameters.Length)
                return false;
            for (int i = 0; i < args.Length; i++)
            {
                var argumentType = args[i];
                var parameterType = runtimeType.RuntimeParameters[i];
                if (parameterType.Type.FullName != argumentType)
                    if (!parameterType.IsAssignableFrom(argumentType))
                        return false;
            }
            return true;
        }
    }
}