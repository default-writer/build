using Build;

namespace UnitTests
{
    namespace Fail_TestSet5
    {
        public interface IOtherRepository
        {
        }

        public class OtherRepository
        {
        }

        //[Dependency("Ho ho ho", RuntimeInstance.CreateInstance)]
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository([Injection(typeof(SqlDataRepository))]ServiceDataRepository repository)
            {
            }

            public Person GetPerson(int personId)
            {
                // get the data from SQL DB and return Person instance.
                return new Person(this);
            }
        }

        [Dependency("Ho ho ho", RuntimeInstance.None)]
        public class ServiceDataRepository : IPersonRepository
        {
            public ServiceDataRepository([Injection("Ho ho ho")]ServiceDataRepository repository)
            {
                Repository = repository;
            }
            public IPersonRepository Repository { get; }
            public Person GetPerson(int personId)
            {
                // get the data from Web service and return Person instance.
                return new Person(this);
            }
        }
    }
}
