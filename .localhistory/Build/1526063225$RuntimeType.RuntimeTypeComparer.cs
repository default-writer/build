using System;
using System.Collections.Generic;

namespace Build
{
    partial class RuntimeType
    {
        class RuntimeTypeComparer : IComparer<Type>
        {
            private RuntimeTypeComparer() { }
            public int Compare(Type x, Type y) => y.IsAssignableFrom(x) ? 1 : -1;
            public static IComparer<Type> Instance { get; } = new RuntimeTypeComparer();
        }
    }
}
