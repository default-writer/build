using Build;

namespace TestSet2
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
        [Dependency(Options.Singleton)]
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

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Ho ho ho", Options.Exclude)]
        public SqlDataRepository()
        {
        }

        private readonly int _personId;

        public SqlDataRepository(int personId)
        {
            _personId = personId;
        }

        public int Id { get {return _personId; } }

        public Person GetPerson(int personId)
        {
            return new Person(this);
        }
    }
}