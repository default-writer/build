using System.Collections.Generic;
using System.Linq;

namespace Build
{
    static class Query
    {
        public static string[] ToStringArray<T>(IEnumerable<T> query) => query?.Select(ToString).ToArray();

        static string ToString<T>(T t) => t?.ToString();
    }
}