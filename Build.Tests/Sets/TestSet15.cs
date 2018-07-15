using System;

namespace Build.Tests.TestSet15
{
    public enum Database
    {
        SQL,
        WebService
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class ErrorWebServiceDataRepository : IPersonRepository
    {
        public ErrorWebServiceDataRepository([Injection(typeof(SqlDataRepository), 2019)]IPersonRepository repository)
        {
            Repository = repository;
            GetPerson(0);
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => Repository.GetPerson(personId);
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
        [Dependency(RuntimeInstance.Singleton)]
        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }

        public Person GetPerson(int personId)
        {
            throw new NotImplementedException();
        }
    }

    public class WebServiceDataRepository : IPersonRepository
    {
        public WebServiceDataRepository([Injection(typeof(SqlDataRepository), 2019)]IPersonRepository repository)
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
}