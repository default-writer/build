//using System;
//using System.Collections.Generic;

//namespace Build
//{
//    /// <summary>
//    /// RuntimeType comparer
//    /// </summary>
//    /// <seealso cref="System.Collections.Generic.IComparer{System.Type}"/>
//    class RuntimeTypeComparer : IComparer<Type>
//    {
//        /// <summary>
//        /// Prevents a default instance of the <see cref="RuntimeTypeComparer"/> class from being created.
//        /// </summary>
//        RuntimeTypeComparer()
//        {
//        }

// /// <summary> /// Gets the instance. /// </summary> /// <value>The instance.</value> public static
// IComparer<Type> Instance { get; } = new RuntimeTypeComparer();

//        /// <summary>
//        /// Compares two objects and returns a value indicating whether one is less than, equal to,
//        /// or greater than the other.
//        /// </summary>
//        /// <param name="x">The first object to compare.</param>
//        /// <param name="y">The second object to compare.</param>
//        /// <returns>
//        /// A signed integer that indicates the relative values of <paramref name="x">x</paramref>
//        /// and <paramref name="y">y</paramref>, as shown in the following table. Value Meaning Less
//        /// than zero <paramref name="x">x</paramref> is less than <paramref name="y">y</paramref>.
//        /// Zero <paramref name="x">x</paramref> equals <paramref name="y">y</paramref>. Greater than
//        /// zero <paramref name="x">x</paramref> is greater than <paramref name="y">y</paramref>.
//        /// </returns>
//        public int Compare(Type x, Type y) => x == y ? 0 : x.IsAssignableFrom(y) ? -1 : 1;
//    }
//}