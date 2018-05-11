namespace Build.Tests.TestSet16
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public enum Database : int
    {
        SQL,
        WebService
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
        public int RepositoryId { get; }
        [Dependency(RuntimeInstance.Singleton)]
        public SqlDataRepository(int repositoryId) => RepositoryId = repositoryId;
        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository : IPersonRepository
    {

        public int RepositoryId { get; }
        public ServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;
        public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
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

    public class WebServiceDataRepository : IPersonRepository
    {
        public int RepositoryId { get; }
        public WebServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;
        public WebServiceDataRepository([Injection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository)
        {
            RepositoryA = repository;
        }
        public WebServiceDataRepository(
            [Injection("Build.Tests.TestSet16.ServiceDataRepository", 2020)]IPersonRepository repositoryA, 
            [Injection("Build.Tests.TestSet16.ServiceDataRepository", 2021)]IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }
        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }
}