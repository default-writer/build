using Build;

namespace UnitTests
{
    namespace TestSet9
    {
        [DependencyAttribute(typeof(SqlDataRepository))]
        public class SqlDataRepository : IPersonRepository
        {

            public SqlDataRepository()
            {
            }

            [DependencyAttribute("Ho ho ho")]
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
