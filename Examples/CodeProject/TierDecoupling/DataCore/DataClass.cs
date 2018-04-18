using DataCore.API;

namespace DataCore
{
    public class DataClass: IData
    {
        public string GetData()
        {
            return "From Data";
        }
    }
}