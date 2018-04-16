using Api;
using Build;

namespace ClassLibraryA
{
    public class A1: IA
    {
        [Dependency(typeof(IA))]
        public A1()
        {
        }
    }
}
