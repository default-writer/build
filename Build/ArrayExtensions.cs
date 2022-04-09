using System;

namespace Build
{
    internal static class ArrayExtensions
    {
        public static T[] ToArray<T>(int count) => new T[count];
#if NET35_OR_GREATER && !NET40_OR_GREATER

        public static T[] Empty<T>() => new T[] { };
#else
        public static T[] Empty<T>() => Array.Empty<T>();

#endif //NET35_OR_GREATER && !NET40_OR_GREATER
    }
}