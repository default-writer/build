namespace Build.Tests.Fail_TestSet6
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
    }

    public class PublicDataRepository : IPersonRepository
    {
        public PublicDataRepository(OtherRepository repository)
        {
            Repository = repository;
        }

        public IOtherRepository Repository { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository(SqlDataRepository repository)
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
        public SqlDataRepository(ServiceDataRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}