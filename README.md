Welcome to the build wiki!

# .NET Core 2.1 Dependency Injection framework

## Latest changes (experimental tag)

* Elimination of overlapped attribute specificators (removes violation of the SOLID principles)
* Circular references detection phase moved to type registeration rather instantiation
* Automatic type resolution for all supprted types if used in type instantiation
* Pure type instantiation as descriptive string with particular constructor and corresponding parameters

## Features

* Declarative metadata attribute driven initialization
* Lazy type resolution and initialization (supports pure dependency decoupling anti-pattern)
* Circular references detection
* Singleton initialization
* Automated and manual type registration
* Type aliases
* External assembly types

The goal of development of this framework is build automation of complex types.

Build can use declarative approach to define dependencies between types and their requirememts.

Constructor injection uses type resolution to resolve devendencies

## Examples

#### Simple load of type registered as default interface implementation from the external assembly

[Load type using interface binding](https://github.com/hack2root/build/blob/master/Examples/AssemblyLoader/Program.cs)

### Create instance with parameters

Usage:
```c#
                var commonPersonContainer = new Container();
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                var sql = new SqlDataRepository();
                var srv1 = (ServiceDataRepository)commonPersonContainer.CreateInstance(
                    "UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
```

Definition:
```c#
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
        }

        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository()
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
            public ServiceDataRepository([Injection]SqlDataRepository repository)
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
```

### Load simple types (not attributes specified)

Usage:
```c#
        IContainer commonPersonContainer = new Container();
        commonPersonContainer.RegisterType<SqlDataRepository>();
        commonPersonContainer.RegisterType<ServiceDataRepository>();

        ServiceDataRepository srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
```

Definition:
```c#
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
        }

        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository()
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
            public ServiceDataRepository(SqlDataRepository repository)
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
```

## Links

[Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1)

[Inversion of Control Containers and the Dependency Injection pattern](https://www.martinfowler.com/articles/injection.html)
