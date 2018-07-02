namespace Build.Tests.TestSet21
{
    using Classes;

    public enum Database
    {
        SQL,
        WebService
    }

    public enum Interface
    {
    }

    public enum Interface2
    {
        Default = -1
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public interface IValueType
    {
        int Value { get; }
    }

    [Interface]
    interface IInterfaceRuleSet
    {
        [InterfaceDependency(RuntimeInstance.Singleton)]
        Type1 Rule(Arg1 arg1, Arg2 arg2);
    }

    [Interface]
    interface IInterfaceRuleSet_CreateInstance
    {
        [InterfaceDependency(RuntimeInstance.CreateInstance)]
        Type1 Rule(Arg1 arg1, Arg2 arg2);
    }

    [Interface]
    interface IInterfaceRuleSet_Enum
    {
        Type2 Rule(Interface myFun);

        Type2 Rule(Interface2 myFun);
    }

    [Interface]
    interface IInterfaceRuleSet_Enum2
    {
        Type3 Rule(Interface2 myFun);
    }

    [Interface]
    interface IInterfaceRuleSet1
    {
        ServiceDataRepository Rule(int repositoryId);

        ServiceDataRepository Rule([InterfaceInjection(typeof(SqlDataRepository), 2018)]IPersonRepository repository);
    }

    [Interface]
    interface IInterfaceRuleSet2
    {
        SqlDataRepository Rule([InterfaceInjection("Build.Tests.TestSet21.ValueType", 2019)]IValueType valueType);

        SqlDataRepository Rule(int value);
    }

    [Interface]
    interface IInterfaceRuleSet2_Overwrite
    {
        SqlDataRepository Rule(
            [InterfaceInjection("Build.Tests.TestSet21.ValueType", 2020)]IValueType valueType);

        SqlDataRepository Rule(int value);
    }

    [Interface]
    interface IInterfaceRuleSet2_ValueType
    {
        ValueType Rule(int value);
    }

    [Interface]
    interface IInterfaceRuleSet3
    {
        WebServiceDataRepository Rule(int repositoryId);

        WebServiceDataRepository Rule([InterfaceInjection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository);

        WebServiceDataRepository Rule(
            [InterfaceInjection("Build.Tests.TestSet21.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
            [InterfaceInjection("Build.Tests.TestSet21.SqlDataRepository", 2021)]IPersonRepository repositoryB);
    }

    [Interface]
    interface IInterfaceRuleSet4
    {
        WebServiceDataRepository2 Rule(int repositoryId);

        WebServiceDataRepository2 Rule([InterfaceInjection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository);

        WebServiceDataRepository2 Rule(
            [InterfaceInjection("Build.Tests.TestSet21.SqlDataRepository", 2020)]IPersonRepository repositoryA,
            [InterfaceInjection("Build.Tests.TestSet21.ServiceDataRepository", 2021)]IPersonRepository repositoryB);
    }

    [Interface]
    interface IInterfaceThisRuleSet1
    {
        ServiceDataRepository this[int repositoryId]
        { get; }

        ServiceDataRepository this[
            [InterfaceInjection(typeof(SqlDataRepository), 2018)]IPersonRepository repository]
        { get; }
    }

    [Interface]
    interface IInterfaceThisRuleSet2
    {
        WebServiceDataRepository this[int repositoryId]
        { get; }

        WebServiceDataRepository this[[InterfaceInjection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository]
        { get; }

        WebServiceDataRepository this[
            [InterfaceInjection("Build.Tests.TestSet21.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
            [InterfaceInjection("Build.Tests.TestSet21.SqlDataRepository", 2021)]IPersonRepository repositoryB]
        { get; }
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository) => _personRepository = personRepository;
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public ServiceDataRepository(IPersonRepository repository) => Repository = repository;

        public IPersonRepository Repository { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId) => new Person(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository(IValueType valueType) => RepositoryId = valueType.Value;

        public SqlDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public int RepositoryId { get; }

        public Person GetPerson(int personId) =>
            // get the data from SQL DB and return Person instance.
            new Person(this);
    }

    public class Type2
    {
        public Type2(Interface myFun) => Interface = myFun;

        public Type2(Interface2 myFun) => Interface2 = myFun;

        public Interface Interface { get; }

        public Interface2 Interface2 { get; }
    }

    public class Type3
    {
        public Type3(Interface2 myFun) => Interface2 = myFun;

        public Interface2 Interface2 { get; }
    }

    public class ValueType : IValueType
    {
        public ValueType(int value) => Value = value;

        public int Value { get; }
    }

    public class WebServiceDataRepository : IPersonRepository
    {
        public WebServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository(IPersonRepository repository) => RepositoryC = repository;

        public WebServiceDataRepository(IPersonRepository repositoryA, IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public IPersonRepository RepositoryC { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId) =>
            // get the data from Web service and return Person instance.
            new Person(this);
    }

    public class WebServiceDataRepository2 : IPersonRepository
    {
        public WebServiceDataRepository2(int repositoryId) => RepositoryId = repositoryId;

        public WebServiceDataRepository2(IPersonRepository repository) => RepositoryA = repository;

        public WebServiceDataRepository2(IPersonRepository repositoryA, IPersonRepository repositoryB)
        {
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        public IPersonRepository RepositoryA { get; }
        public IPersonRepository RepositoryB { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId) =>
            // get the data from Web service and return Person instance.
            new Person(this);
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