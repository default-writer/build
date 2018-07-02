namespace Build.Tests.Fail_TestSet7
{
    public interface IOtherRepository : IPersonRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class OtherRepository : IOtherRepository
    {
        [Dependency(typeof(IOtherRepository))]
        public OtherRepository(int personId)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
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
        public ServiceDataRepository([Injection("Build.Tests.Fail_TestSet7.IOtherRepository", 2018)]IPersonRepository repository)
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
        [Dependency("Build.Tests.Fail_TestSet7.IOtherRepository")]
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