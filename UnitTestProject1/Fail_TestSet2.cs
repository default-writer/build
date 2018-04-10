using Build;

namespace UnitTests
{
    namespace Fail_TestSet2
    {
        [Dependency(typeof(IPersonRepository))]
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
            public ServiceDataRepository([Injection(typeof(IPersonRepository))]SqlDataRepository repository)
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
