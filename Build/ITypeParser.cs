using System.Collections.Generic;

namespace Build
{
    public interface ITypeParser
    {
        IRuntimeType Find(string type, string[] args, IEnumerable<IRuntimeType> types);
    }
}