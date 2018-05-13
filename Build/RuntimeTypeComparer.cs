using System;
using System.Collections.Generic;

namespace Build
{
    class RuntimeTypeComparer : IComparer<Type>
    {
        RuntimeTypeComparer()
        {
        }

        public static IComparer<Type> Instance { get; } = new RuntimeTypeComparer();

        public int Compare(Type x, Type y) => y.IsAssignableFrom(x) ? 1 : -1;
    }
}