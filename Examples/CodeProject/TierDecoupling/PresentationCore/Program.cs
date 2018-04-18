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
            Container container = new Container();

            Assembly dataClasses = Assembly.LoadFrom("DataCore.dll");
            Assembly businessClasses = Assembly.LoadFrom("BusinessCore.dll");

            container.RegisterAssemblyTypes(dataClasses);
            container.RegisterAssemblyTypes(businessClasses);

            IBusiness business = container.CreateInstance<IBusiness>();
            string data = business.GetBusinessData();

            Console.WriteLine(data);
            Console.ReadLine();
        }
    }
}
