using Build;

namespace TestSet3
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

        public IPersonRepository Repository { get { return _personRepository; } }
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

        public ServiceDataRepository()
        {
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository2 : IPersonRepository
    {
        public ServiceDataRepository2([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Ho ho ho")]
        public SqlDataRepository()
        {
        }

        [Dependency(Options.Singleton)]
        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId) => new(this);
    }
}