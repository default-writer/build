using Api;
using Build;
using System;
using System.Reflection;

namespace AssemblyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //building external types

            //Lib1 depends on Api and Build type resolution
            var lib1 = Assembly.LoadFrom("Lib1.dll");
            var lib2 = Assembly.LoadFrom("Lib2.dll");

            var container = new Container();
            container.RegisterAssemblyTypes(lib1);
            container.RegisterAssemblyTypes(lib2);

            // Loads A1 type registered as an interface IA default implementation
            var a = container.CreateInstance<IA>();
            Console.WriteLine(a?.ToString() ?? "null");
            Console.ReadLine();
        }
    }
}
