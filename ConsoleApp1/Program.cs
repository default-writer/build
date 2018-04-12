namespace Console
{
    using Build;

    internal class Program
    {
        static void Main(string[] args)
        {
            IContainer commonPersonContainer = new Container();
            commonPersonContainer.RegisterType<SqlDataRepository>();
            commonPersonContainer.RegisterType<ServiceDataRepository>();

            //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
            var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
            var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
        }
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
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

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection(typeof(SqlDataRepository))]IPersonRepository repository)
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

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }
}
