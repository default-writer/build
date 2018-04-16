using Api;
using Build;

namespace ClassLibraryA
{
    //[Dependency(typeof(IA))]
    public class A1: IA
    {
        [DependencyAttribute(typeof(IA))]
        public A1()
        {
        }
    }
}
