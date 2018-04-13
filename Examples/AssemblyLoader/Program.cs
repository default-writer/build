using System;
using System.Reflection;

namespace AssemblyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Assembly loader for building types
            Assembly alib = Assembly.LoadFrom("Lib1.dll");
            Assembly blib = Assembly.LoadFrom("Lib2.dll");
        }
    }
}
