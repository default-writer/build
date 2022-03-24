#pragma warning disable IDE0060

using System;
using Build;

namespace Fail_TestSet3
{
    public interface IOtherRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
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

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection(typeof(IOtherRepository))]IOtherRepository repository)
        {
            Repository = repository;
        }

        public IOtherRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository()
        {
        }

        [Dependency(typeof(IOtherRepository))]
        public SqlDataRepository(int personId)
        {
        }

        public Person GetPerson(int personId) => new(this);
    }

    class OtherRepository
    {
    }
}