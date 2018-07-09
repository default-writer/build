namespace Build.Tests.Fail_TestSet2
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

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection("Build.Tests.Fail_TestSet2.IPersonRepository")]int repositoryId) => RepositoryId = repositoryId;

        public IPersonRepository Repository { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Build.Tests.Fail_TestSet2.IPersonRepository")]
        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public SqlDataRepository()
        {
        }

        public int PersonId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}