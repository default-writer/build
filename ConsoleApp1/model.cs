using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    [Dependency(typeof(SqlDataRepository))]
    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository()
        {
        }

        [Dependency("SqlDataRepository2", Runtime.Singleton)]
        public SqlDataRepository(int personId)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    [Dependency(Runtime.Singleton)]
    public class ServiceDataRepository : IPersonRepository
    {
        IPersonRepository _repository;
        public ServiceDataRepository([Inject(typeof(SqlDataRepository))]IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class Person
    {
        private readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }
}
