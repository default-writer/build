using Build;

namespace TestSet16
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
        public Person(IPersonRepository personRepository)
        {
            Repository = personRepository;
        }

        public IPersonRepository Repository { get; }
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

    public class ServiceDataRepository2 : IPersonRepository
    {
        public ServiceDataRepository2(int repositoryId) => RepositoryId = repositoryId;

        public ServiceDataRepository2([Injection(typeof(SqlDataRepository), 2018)] IPersonRepository repository)
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
        [Dependency(Options.Singleton)]
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
            [Injection("TestSet16.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
            [Injection("TestSet16.SqlDataRepository", 2021)]IPersonRepository repositoryB)
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
            [Injection("TestSet16.SqlDataRepository", 2020)]IPersonRepository repositoryA,
            [Injection("TestSet16.ServiceDataRepository", 2021)]IPersonRepository repositoryB)
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

    public class WebServiceDataRepository3 : IPersonRepository
    {
        public WebServiceDataRepository3(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository3([Injection(typeof(ServiceDataRepository), 2019)] IPersonRepository repository)
        {
            RepositoryA = repository;
        }

        public WebServiceDataRepository3(
            IPersonRepository repositoryA,
            [Injection("TestSet16.ServiceDataRepository", 2021)] IPersonRepository repositoryB)
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