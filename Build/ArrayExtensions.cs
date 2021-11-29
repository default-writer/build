using System;

namespace Build
{
    internal static class ArrayExtensions
    {
        public static T[] ToArray<T>(int count) => new T[count];

        public static T[] Empty<T>() => Array.Empty<T>();
    }
}