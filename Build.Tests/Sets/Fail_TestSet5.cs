using Build;

namespace Fail_TestSet5
{
    public interface IOtherRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class NoSqlDataRepository
    {
        public IOtherRepository Repository { get; }

        public NoSqlDataRepository([Injection(typeof(OtherRepository), 2018)]IOtherRepository other)
        {
            Repository = other;
        }

        public static Person GetPerson()
        {
            // get the data from SQL DB and return Person instance.
            return new Person(null);
        }
    }

    public class OtherRepository : NoSqlDataRepository, IOtherRepository
    {
        [Dependency(RuntimeInstance.Exclude)]
        public OtherRepository(int param) : base(null)
        {
            Parameter = param;
        }

        public int Parameter { get; }
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
        [Dependency("Ho ho ho", RuntimeInstance.Exclude)]
        public ServiceDataRepository([Injection("Ho ho ho")]ServiceDataRepository repository)
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
        public SqlDataRepository([Injection(typeof(SqlDataRepository))]ServiceDataRepository repository)
        {
            Repository = repository;
        }

        public ServiceDataRepository Repository { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}