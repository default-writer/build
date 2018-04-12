using Build;

namespace UnitTests
{
    namespace Fail_TestSet7
    {
        public interface IOtherRepository: IPersonRepository
        {   
        }

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

        [Dependency("UnitTests.Fail_TestSet7.IOtherRepository")]
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

        [Dependency(typeof(IOtherRepository))]
        public class OtherRepository : IOtherRepository
        {
            public OtherRepository(int personId)
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
            public ServiceDataRepository([Injection("UnitTests.Fail_TestSet7.IOtherRepository, UnitTests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]IPersonRepository repository)
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
