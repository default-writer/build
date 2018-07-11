using System;

namespace Build
{
    public static class ObjectArray
    {
        public static object[] Empty() => TypeHolder<object>.EmptyArray;
    }

    public static class StringArray
    {
        public static string[] Empty() => TypeHolder<string>.EmptyArray;
    }

    public static class TypeArray
    {
        public static Type[] Empty() => TypeHolder<Type>.EmptyArray;
    }

    static class TypeHolder<T>
    {
        static TypeHolder() => EmptyArray = new T[0];

        public static T[] EmptyArray { get; }
    }
}