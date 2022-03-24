#pragma warning disable IDE0060

using System;
using Build;

namespace Fail_TestSet7
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
        private readonly int _personId;

        [Dependency(typeof(IOtherRepository))]
        public OtherRepository(int personId)
        {
            _personId = personId;
        }

        public int Id { get {return _personId; } }

        public Person GetPerson(int personId) => new(this);
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
        public ServiceDataRepository([Injection("Fail_TestSet7.IOtherRepository", 2018)]IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Fail_TestSet7.IOtherRepository")]
        public SqlDataRepository(int personId)
        {
        }

        public Person GetPerson(int personId) => new(this);
    }
}