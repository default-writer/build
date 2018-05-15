using System.Collections.Generic;

namespace Build
{
    class Format
    {
        public static string GetConstructorFullName(string constructor, IEnumerable<string> parameters) => string.Format("{0}({1})", constructor, string.Join(",", parameters));

        public static string GetConstructorParameterFullName(string typeFullName, int? i) => string.Format("{0}:({1})", typeFullName, i);
    }
}