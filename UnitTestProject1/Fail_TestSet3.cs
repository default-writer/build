using Build;

namespace UnitTests
{
    namespace Fail_TestSet3
    {
        public interface IOtherRepository
        {
        }

        [Dependency(typeof(IOtherRepository))]
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository(int personId)
            {
            }

            public Person GetPerson(int personId)
            {
                // get the data from SQL DB and return Person instance.
                return new Person(this);
            }
        }
        
        public class ServiceDataRepository : IPersonRepository
        {
            public ServiceDataRepository([Injection(typeof(IOtherRepository))]IOtherRepository repository)
            {
                Repository = repository;
            }
            public IOtherRepository Repository { get; }
            public Person GetPerson(int personId)
            {
                // get the data from Web service and return Person instance.
                return new Person(this);
            }
        }
    }
}
