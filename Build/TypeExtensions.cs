using System.Collections.Generic;
using System.Linq;

namespace Build
{
    internal static class TypeExtensions
    {
        public static string[] ToStringArray<T>(this IEnumerable<T> query) => query?.Select(ToString).ToArray();

        private static string ToString<T>(this T t) => t?.ToString();
    }
}