using Build;
using BusinessCore.API;
using System;
using System.Reflection;

namespace PresentationCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            var dataClasses = Assembly.LoadFrom("DataCore.dll");
            var businessClasses = Assembly.LoadFrom("BusinessCore.dll");

            container.RegisterAssemblyTypes(dataClasses);
            container.RegisterAssemblyTypes(businessClasses);

            var business = container.CreateInstance<IBusiness>();
            string data = business.GetBusinessData();

            Console.WriteLine(data);
            Console.ReadLine();
        }
    }
}
