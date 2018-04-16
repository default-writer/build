using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Build
{
    public class TypeParser : ITypeParser
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
                if (p.Parameters.Length != args.Count)
                    return false;
                for (int i = 0; i < args.Count; i++)
                    if (args[i].Value.Trim() != p.Parameters[i].Id)
                        return false;
                return true;
            });
            return type;
        }
    }
}