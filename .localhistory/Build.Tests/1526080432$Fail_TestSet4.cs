namespace Build.Tests.Fail_TestSet4
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class Person
    {
        private readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }

    public interface IOtherRepository
    {
    }

    public class OtherRepository
    {
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository([Injection(typeof(SqlDataRepository))]SqlDataRepository repository)
        {
        }

        [Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository")]
        public SqlDataRepository([Injection("Build.Tests.Fail_TestSet4.ServiceDataRepository")]ServiceDataRepository repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository))]
        public ServiceDataRepository(SqlDataRepository repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }
}