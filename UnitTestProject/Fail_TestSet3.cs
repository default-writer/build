using Build;

namespace UnitTests
{
    namespace Fail_TestSet3
    {
        public interface IPersonRepository
        {
            Person GetPerson(int personId);
        }

        public class Person
        {
            readonly IPersonRepository _personRepository;

            public Person(IPersonRepository personRepository)
            {
                _personRepository = personRepository;
            }
        }

        public interface IOtherRepository
        {
        }

        class OtherRepository
        {
        }

        public class SqlDataRepository : IPersonRepository
        {
            [Dependency(typeof(IOtherRepository))]
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
