namespace Build.Tests.TestSet16
{
    public enum Database
    {
        SQL,
        WebService
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

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

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
        [Dependency(RuntimeInstance.Singleton)]
        public SqlDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class WebServiceDataRepository : IPersonRepository
    {
        public WebServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository([Injection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository)
        {
            RepositoryA = repository;
        }

        public WebServiceDataRepository(
            [Injection("Build.Tests.TestSet16.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
            [Injection("Build.Tests.TestSet16.SqlDataRepository", 2021)]IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class WebServiceDataRepository2 : IPersonRepository
    {
        public WebServiceDataRepository2(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository2([Injection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository)
        {
            RepositoryA = repository;
        }

        public WebServiceDataRepository2(
            [Injection("Build.Tests.TestSet16.SqlDataRepository", 2020)]IPersonRepository repositoryA,
            [Injection("Build.Tests.TestSet16.ServiceDataRepository", 2021)]IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }
}