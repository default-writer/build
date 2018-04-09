using System;

namespace Pointer
{
    using ConsoleApp1;
    using System.Reflection;

    class Program
    {

        static void Main(string[] args)
        {
            //Container personContainer = new Container();
            //personContainer.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            //SqlDataRepository sqlRepo = (SqlDataRepository)personContainer.CreateInstance<SqlDataRepository>();
            //IPersonRepository srvRepo = personContainer.CreateInstance<ServiceDataRepository>();

            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<SqlDataRepository>();
            commonPersonContainer.RegisterType<ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            ServiceDataRepository srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
            ServiceDataRepository srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();

            Console.WriteLine("Hello World!");
        }
    }
}
