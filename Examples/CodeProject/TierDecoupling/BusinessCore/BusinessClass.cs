using Build;
using BusinessCore.API;
using DataCore.API;

namespace BusinessCore
{
    public class BusinessClass: IBusiness
    {
        IData _dataClass;
        [Dependency(typeof(IBusiness))]
        public BusinessClass([Injection("DataCore.DataClass")]IData data)
        {
            _dataClass = data;
        }
        public string GetBusinessData()
        {
            return _dataClass.GetData();
        }
    }
}
