namespace Build
{
    static class Array
    {
        public static T[] Empty<T>() => TypeHolder<T>.EmptyArray;

        public static T[] Empty<T>(int count) => TypeHolder<T>.GetEmptyArray(count);
    }

    static class TypeHolder<T>
    {
        public static T[] EmptyArray { get; } = Array.Empty<T>(0);

        public static T[] GetEmptyArray(int count) => new T[count];
    }
}