using System.Collections.Generic;

namespace Build
{
    public interface ITypeParser
    {
        IRuntimeType Find(string id, IEnumerable<IRuntimeType> args);
    }
}