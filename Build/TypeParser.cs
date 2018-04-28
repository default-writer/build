using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Build
{
    class TypeParser : ITypeParser
    {
        public IRuntimeType Find(string id, IEnumerable<IRuntimeType> types)
        {
            var func = Regex.Match(id, @"([^()]+)(?:\((.*)\)){0,1}$");
            var name = func.Groups[1].Value.Trim();
            var pars = func.Groups[2].Value.Trim();
            var args = Regex.Matches(pars, @"([^,]+\(.+?\))|([^,]+)");
            var type = types.FirstOrDefault((p) =>
            {
                if (p.Id != name)
                    return false;
                if (args.Count > 0 && p.RuntimeParameters != null && p.RuntimeParameters.Length != args.Count)
                    return false;
                for (int i = 0; i < args.Count; i++)
                {
                    string argumentType = args[i].Value.Trim();
                    if (p.RuntimeParameters[i].Id != argumentType)
                    {
                        var parameterType = p.RuntimeParameters[i].Type;
                        var assignableType = p.RuntimeParameters[i].FindParameteTyper(argumentType);
                        if (assignableType == null || !assignableType.IsAssignableFrom(parameterType))
                            return false;
                    }
                }
                return true;
            });
            return type;
        }
    }
}