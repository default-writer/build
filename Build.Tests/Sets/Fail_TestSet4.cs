using Build;

namespace Fail_TestSet4
{
    public interface IOtherRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class OtherRepository
    {
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
        [Dependency(typeof(SqlDataRepository))]
        public ServiceDataRepository(SqlDataRepository repository)
        {
            Repository = repository;
        }

        public SqlDataRepository Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository2 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository2))]
        public ServiceDataRepository2(object repository)
        {
            Repository = repository;
        }

        public object Repository { get; }
        
        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository3 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository3))]
        public ServiceDataRepository3(SqlDataRepository3 repository)
        {
            Repository = repository;
        }

        public SqlDataRepository3 Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository4 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository4))]
        public ServiceDataRepository4(object repository)
        {
            Repository = repository;
        }

        public object Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository5 : IPersonRepository
    {
        //[Dependency(typeof(SqlDataRepository))]
        public ServiceDataRepository5(SqlDataRepository5 repository)
        {
            Repository = repository;
        }

        public SqlDataRepository5 Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository6 : IPersonRepository
    {
        [Dependency("Fail_TestSet4.SqlDataRepository6")]
        public ServiceDataRepository6(object repository)
        {
            Repository = repository;
        }

        public object Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class ServiceDataRepository7 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository7))]
        public ServiceDataRepository7(object repository)
        {
            Repository = repository;
        }

        public object Repository { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository([Injection(typeof(SqlDataRepository))]SqlDataRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }

        [Dependency("Fail_TestSet4.SqlDataRepository")]
        public SqlDataRepository([Injection("Fail_TestSet4.ServiceDataRepository")]ServiceDataRepository repository, int value)
        {
            Repository = repository;
            Value = value;
        }

        public int Value { get; }
        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository2 : IPersonRepository
    {
        public SqlDataRepository2([Injection(typeof(SqlDataRepository2))]object repository)
        {
            Repository = repository;
        }

        [Dependency("Fail_TestSet4.SqlDataRepository2")]
        public SqlDataRepository2([Injection("Fail_TestSet4.ServiceDataRepository2")]ServiceDataRepository repository, int value)
        {
            Repository = repository;
            Value = value;
        }

        public object Repository { get; }
        public int Value { get; }
        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository3 : IPersonRepository
    {
        public SqlDataRepository3([Injection(typeof(object))]SqlDataRepository3 repository)
        {
            Repository = repository;
        }

        [Dependency("Fail_TestSet4.SqlDataRepository3")]
        public SqlDataRepository3([Injection("Fail_TestSet4.ServiceDataRepository3")]ServiceDataRepository repository, int value)
        {
            Repository = repository;
            Value = value;
        }

        public IPersonRepository Repository { get; }
        public int Value { get; }


        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository4 : IPersonRepository
    {
        public SqlDataRepository4([Injection(typeof(object))]object repository)
        {
            Repository = repository;
        }

        [Dependency("Fail_TestSet4.ServiceDataRepository4")]
        public SqlDataRepository4(object repository, int value)
        {
            Repository = repository;
            Value = value;
        }

        public object Repository { get; }
        public int Value { get; }


        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository5 : IPersonRepository
    {
        public SqlDataRepository5(int personId)
        {
            Id = personId;
        }

        public int Id { get; }

        public SqlDataRepository5([Injection("Fail_TestSet4.ServiceDataRepository5")]object repository, int value)
        {
            Repository = repository;
            Value = value;
        }

        public object Repository { get; }
        public int Value { get; }


        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository6 : IPersonRepository
    {
        public SqlDataRepository6([Injection(typeof(object))]object repository)
        {
            Repository = repository;
        }

        //[Dependency("Fail_TestSet4.SqlDataRepository")]
        public SqlDataRepository6([Injection(typeof(ServiceDataRepository6))]object repository, int value)
        {
            Repository = repository;
            Id = value;
        }

        public object Repository { get; }

        public int Id { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository7 : IPersonRepository
    {
        [Dependency("Fail_TestSet4.ServiceDataRepository7")]
        public SqlDataRepository7(int personId)
        {
            Id = personId;
        }

        public int Id { get; }

        public Person GetPerson(int personId) => new(this);
    }
}