namespace Build
{
    static class ArrayExtensions
    {
        public static T[] ToArray<T>(int count = 0) => new T[count];
    }
}