using Build;

namespace UnitTests
{
    namespace TestSet12
    {
        [Dependency("Ho ho ho", RuntimeInstance.None)]
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository()
            {
            }

            [Dependency(RuntimeInstance.Singleton)]
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
            public ServiceDataRepository(SqlDataRepository repository)
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
