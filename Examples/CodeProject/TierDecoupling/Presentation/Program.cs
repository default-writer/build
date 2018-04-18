using System;

namespace Presentation
{
  class Program
  {
    static void Main(string[] args)
    {
      Business.BusinessClass businessClass = new Business.BusinessClass();
      string data = businessClass.GetBusinessData();
      Console.WriteLine(data);
      Console.ReadLine();
    }
  }
}
