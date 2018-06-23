namespace Build.Tests.TestSet7
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public interface IPersonRepository2
    {
    }

    public class OtherRepository : IPersonRepository2
    {
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
        public ServiceDataRepository([Injection("Ho ho ho")]IPersonRepository repository)
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

    public class ServiceDataRepository2 : IPersonRepository
    {
        public Person GetPerson(int personId) => new Person(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Ho ho ho")]
        public SqlDataRepository()
        {
        }

        public SqlDataRepository(int personId)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}