namespace Build
{
    static class Array
    {
        public static T[] Empty<T>() => TypeHolder<T>.EmptyArray;

        public static T[] Empty<T>(int count) => TypeHolder<T>.GetEmptyArray(count);
    }

    static class TypeHolder<T>
    {
        static TypeHolder() => EmptyArray = new T[0];

        public static T[] EmptyArray { get; }

        public static T[] GetEmptyArray(int count) => new T[count];
    }
}