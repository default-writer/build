using System;

namespace Presentation
{
  class Program
  {
    static void Main(string[] args)
    {
      var businessClass = new Business.BusinessClass();
      string data = businessClass.GetBusinessData();
      Console.WriteLine(data);
      Console.ReadLine();
    }
  }
}
