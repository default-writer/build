using System;
using System.Collections.Generic;

namespace Build
{
    internal class RuntimeTypeComparer : IComparer<Type>
    {
        private RuntimeTypeComparer()
        {
        }

        public static IComparer<Type> Instance { get; } = new RuntimeTypeComparer();

        public int Compare(Type x, Type y) => y.IsAssignableFrom(x) ? 1 : -1;
    }
}