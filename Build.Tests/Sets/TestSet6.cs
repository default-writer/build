using Build;

namespace TestSet6
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
        public ServiceDataRepository([Injection("Ho ho ho", 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        private readonly int _personId;

        public SqlDataRepository()
        {
        }

        [Dependency("Ho ho ho")]

        public SqlDataRepository(int personId)
        {
            _personId = personId;
        }

        public int Id { get {return _personId; } }

        public Person GetPerson(int personId) => new(this);
    }
}