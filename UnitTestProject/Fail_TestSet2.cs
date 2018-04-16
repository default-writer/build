using Build;

namespace UnitTests
{
    namespace Fail_TestSet2
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

        public class SqlDataRepository : IPersonRepository
        {
            [Dependency("UnitTests.Fail_TestSet2.IPersonRepository")]
            public SqlDataRepository(int personId)
            {
            }

            public SqlDataRepository()
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
            public ServiceDataRepository([Injection("UnitTests.Fail_TestSet2.IPersonRepository")]int repository)
            {
                //Repository = repository;
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
