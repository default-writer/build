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

        //[Dependency(typeof(IPersonRepository))]
        [Dependency("UnitTests.Fail_TestSet2.IPersonRepository")]
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
            public ServiceDataRepository([Injection("UnitTests.Fail_TestSet2.IPersonRepository, UnitTests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]int repository)
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
