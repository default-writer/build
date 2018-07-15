using System;

namespace Build.Tests.Fail_TestSet1
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class ErrorClass
    {
        public ErrorClass(IdType type) => Id = type.Id;

        public string Id { get; }
    }

    public class ErrorServiceDataRepository2 : IPersonRepository
    {
        public ErrorServiceDataRepository2([Injection("Build.Tests.Fail_TestSet1.Other2")]IPersonRepository repository)
        {
            throw new NotImplementedException();
        }

        public IPersonRepository Repository { get; }

        public Person GetPerson(int personId) => new Person(this);
    }

    public class IdType
    {
        public string Id { get; set; }
    }

    public class Other : IPersonRepository
    {
        Other()
        { }

        public Person GetPerson(int personId) => throw new System.NotImplementedException();
    }

    public class Other2 : IPersonRepository
    {
        [Dependency(RuntimeInstance.Exclude)]
        public Other2(Type type)
        {
        }

        Other2()
        { }

        public Person GetPerson(int personId) => throw new System.NotImplementedException();
    }

    public abstract class Other5 : IPersonRepository
    {
        [Dependency(RuntimeInstance.Exclude)]
        protected Other5(Type type)
        {
        }

        Other5()
        { }

        public Person GetPerson(int personId) => throw new System.NotImplementedException();
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }

    public class PrivateConstructorServiceDataRepository : IPersonRepository
    {
        PrivateConstructorServiceDataRepository(IPersonRepository repository)
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

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection("Build.Tests.Fail_TestSet1.Other")]IPersonRepository repository)
        {
            Repository = repository;
        }

        public ServiceDataRepository([Injection(typeof(Other2))]Other2 repository)
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

    public class ServiceDataRepository2 : IPersonRepository
    {
        public ServiceDataRepository2([Injection("Build.Tests.Fail_TestSet1.Other2")]IPersonRepository repository)
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

    public abstract class ServiceDataRepository3 : IPersonRepository
    {
        protected ServiceDataRepository3([Injection("Build.Tests.Fail_TestSet1.Other2", typeof(PrivateConstructorServiceDataRepository))]IPersonRepository repository)
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

    public class ServiceDataRepository4 : IPersonRepository
    {
        public ServiceDataRepository4([Injection("Build.Tests.Fail_TestSet1.Other4")]IPersonRepository repository)
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

    public class ServiceDataRepository5 : IPersonRepository
    {
        public ServiceDataRepository5([Injection("Build.Tests.Fail_TestSet1.Other5")]IPersonRepository repository)
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
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}