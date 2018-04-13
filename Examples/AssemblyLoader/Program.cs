using Build;
using System;
using System.Reflection;

namespace AssemblyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Assembly loader for building types
            Assembly lib1 = Assembly.LoadFrom("Lib1.dll");
            Assembly lib2 = Assembly.LoadFrom("Lib2.dll");

            Container container = new Container();
            container.RegisterAssemblyTypes(lib1);
            container.RegisterAssemblyTypes(lib2);
        }
    }
}
