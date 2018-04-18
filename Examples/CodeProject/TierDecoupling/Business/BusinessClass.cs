using Data;
namespace Business
{
  public class BusinessClass
  {
    DataClass _dataClass;
    public BusinessClass()
    {
       _dataClass = new DataClass();
    }
    public string GetBusinessData()
    {
      return _dataClass.GetData();
    }
  }
}
