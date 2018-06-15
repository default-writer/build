namespace Build.Tests.TestSet1
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository) => _personRepository = personRepository;
    }

    public class PrivateSqlDataRepository : IPersonRepository
    {
        public PrivateSqlDataRepository()
        {
        }

        public PrivateSqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository : IPersonRepository
    {
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
        public SqlDataRepository()
        {
        }

        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    class CircularReference1
    {
        [Dependency("#1")]
        public CircularReference1([Injection("#2")]object person)
        {
            PersonId = ((CircularReference3)person).PersonId;
        }

        public int PersonId { get; }
    }

    class CircularReference2
    {
        [Dependency("#2")]
        public CircularReference2([Injection("#3")]object person)
        {
            PersonId = ((CircularReference1)person).PersonId;
        }

        public int PersonId { get; }
    }

    class CircularReference3
    {
        [Dependency("#3")]
        public CircularReference3([Injection("#1")]object person)
        {
            PersonId = ((CircularReference2)person).PersonId;
        }

        public int PersonId { get; }
    }

    class PrivateSqlDataRepository2 : IPersonRepository
    {
        public PrivateSqlDataRepository2([Injection(typeof(CircularReference2))]object reference)
        {
            PersonId = reference != null ? ((CircularReference2)reference).PersonId : 0;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}