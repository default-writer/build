using System;
using System.Collections.Generic;
using System.Linq;

namespace Build
{
    public static class TypeParserExtensions
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public static IRuntimeType Find(this ITypeParser typeParser, string id, IEnumerable<string> args, IEnumerable<IRuntimeType> types) => typeParser.FindRuntimeTypes(id, args, types).FirstOrDefault();
    }
}