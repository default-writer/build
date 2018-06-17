namespace Build.Interfaces.Tests
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

    public interface IValueType
    {
        int Value { get; }
    }

    [MyFun]
    interface IMyFunRuleSet
    {
        Type1 Rule(Arg1 arg1, Arg2 arg2);
    }

    [MyFun]
    interface IMyFunRuleSet1
    {
        ServiceDataRepository Rule([MyFunInjection(typeof(SqlDataRepository), 2018)]IPersonRepository repository);

        ServiceDataRepository Rule(int repositoryId);
    }

    [MyFun]
    interface IMyFunRuleSet2
    {
        SqlDataRepository Rule(
            [MyFunInjection("Build.Interfaces.Tests.ValueType", 2019)]IValueType valueType);

        SqlDataRepository Rule(int value);
    }

    [MyFun]
    interface IMyFunRuleSet2_Overwrite
    {
        SqlDataRepository Rule(
            [MyFunInjection("Build.Interfaces.Tests.ValueType", 2020)]IValueType valueType);

        SqlDataRepository Rule(int value);
    }

    [MyFun]
    interface IMyFunRuleSet2_ValueType
    {
        ValueType Rule(int value);
    }

    [MyFun]
    interface IMyFunRuleSet3
    {
        WebServiceDataRepository Rule(int repositoryId);

        WebServiceDataRepository Rule([MyFunInjection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository);

        WebServiceDataRepository Rule(
            [MyFunInjection("Build.Interfaces.Tests.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
            [MyFunInjection("Build.Interfaces.Tests.SqlDataRepository", 2021)]IPersonRepository repositoryB);
    }

    [MyFun]
    interface IMyFunRuleSet4
    {
        WebServiceDataRepository2 Rule(int repositoryId);

        WebServiceDataRepository2 Rule([MyFunInjection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository);

        WebServiceDataRepository2 Rule(
            [MyFunInjection("Build.Interfaces.Tests.SqlDataRepository", 2020)]IPersonRepository repositoryA,
            [MyFunInjection("Build.Interfaces.Tests.ServiceDataRepository", 2021)]IPersonRepository repositoryB);
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
        public ServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public ServiceDataRepository(IPersonRepository repository)
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository(IValueType valueType) => RepositoryId = valueType.Value;

        public SqlDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class ValueType : IValueType
    {
        public ValueType(int value) => Value = value;

        public int Value { get; }
    }

    public class WebServiceDataRepository : IPersonRepository
    {
        public WebServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository(IPersonRepository repository)
        {
            RepositoryC = repository;
        }

        public WebServiceDataRepository(IPersonRepository repositoryA, IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public IPersonRepository RepositoryC { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class WebServiceDataRepository2 : IPersonRepository
    {
        public WebServiceDataRepository2(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository2(IPersonRepository repository)
        {
            RepositoryA = repository;
        }

        public WebServiceDataRepository2(IPersonRepository repositoryA, IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    class Arg1
    {
    }

    class Arg2
    {
    }

    class Type1
    {
        public Type1(Arg1 arg1, Arg2 arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }

        public Arg1 Arg1 { get; }

        public Arg2 Arg2 { get; }
    }
}