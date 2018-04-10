using Build;

namespace UnitTests
{
    namespace TestSet3
    {
        [DependencyAttribute(RuntimeInstance.Singleton)]
        public class SqlDataRepository : IPersonRepository
        {
            [DependencyAttribute("Ho ho ho")]
            public SqlDataRepository()
            {
            }

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
            public ServiceDataRepository([Injection(typeof(SqlDataRepository))]IPersonRepository repository)
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
