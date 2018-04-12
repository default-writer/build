using Build;

namespace UnitTests
{
    namespace Fail_TestSet6
    {
        public interface IOtherRepository
        {
        }

        public class OtherRepository
        {
        }

        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository(ServiceDataRepository repository)
            {
                Repository = repository;
            }
            public IPersonRepository Repository { get; }
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
