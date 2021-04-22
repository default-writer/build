namespace Fail_TestSet6
{
    public interface IOtherRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class OtherRepository : IOtherRepository
    {
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

    public class PublicDataRepository : IPersonRepository
    {
        public PublicDataRepository(OtherRepository repository)
        {
            Repository = repository;
        }

        public IOtherRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository(SqlDataRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository(ServiceDataRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }
}