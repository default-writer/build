Welcome to the build wiki!

# .NET Core 2.1 Dependency Injection framework

## Repository

[build](https://github.com/hack2root/build)

## Docs 

[wiki](https://github.com/hack2root/build/wiki)

## Continious Integration

- [CircleCI](https://circleci.com) integration
- [TravisCI](https://travis-ci.org) integration

[![CircleCI](https://circleci.com/gh/hack2root/build.svg?style=shield)](https://circleci.com/gh/hack2root/build)
[![Build Status](https://travis-ci.org/hack2root/build.svg?branch=master)](https://travis-ci.org/hack2root/build)

## v1.0.0.1

### Major improvements

- Added support for default parameterless constructor with parameters injection using attributes

### Examples

### Default constructor with parameters injection

Usage:
```c#
var container = new Container();
container.RegisterType<SqlDataRepository>();
container.RegisterType<ServiceDataRepository>();
var srv = (ServiceDataRepository)container.CreateInstance(
    "UnitTests.TestSet15.ServiceDataRepository(UnitTests.TestSet15.IPersonRepository)");
```

Definition:

```c#
public class SqlDataRepository : IPersonRepository
{
    public int PersonId { get; }

    public SqlDataRepository(int personId)
    {
        PersonId = personId;
    }

    public Person GetPerson(int personId)
    {
        // get the data from SQL DB and return Person instance.
        return new Person(this);
    }
}

public class ServiceDataRepository : IPersonRepository
{
    public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]
      IPersonRepository repository)
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

## Description

* Interface first-class support (instantiation, strong typing, external assembly)
* Circular references detection in type registration
* Automatic type resolution for all supported types
* Pure type instantiation (weak coupling)
* Declarative metadata attribute driven initialization
* Lazy type resolution and initialization (supports pure dependency injection decoupling anti-pattern)
* Singleton initialization support
* Automated and manual type registration
* Types aliases (user-friendly type identification)
* External assembly types support

## Latest changes (experimental tag, 1.0.0.0)

* Elimination of overlapped attribute specificators (removes violation of the SOLID principles)
* Circular references detection phase moved to type registration rather instantiation
* Automatic type resolution for all supported types if used in type instantiation
* Pure type instantiation as descriptive string with particular constructor and passing corresponding parameters

## Features

* Declarative metadata attribute driven initialization
* Lazy type resolution and initialization (supports pure dependency decoupling anti-pattern)
* Circular references detection
* Singleton initialization
* Automated and manual type registration
* Type aliases
* External assembly types

## Goal 

The goal of development of this framework is to build automation of complex types initialization.
Build can use declarative approach to define dependencies between types and their requirements.
Constructor injection uses type resolution to resolve devendencies

## Examples

### Create instance with parameters

Usage:
```c#
var container = new Container();
container.RegisterType<SqlDataRepository>();
container.RegisterType<ServiceDataRepository>();
var sql = new SqlDataRepository();
var srv1 = (ServiceDataRepository)container.CreateInstance(
    "UnitTests.TestSet14.ServiceDataRepository(UnitTests.TestSet14.SqlDataRepository)", sql);
```

### Load simple types (not attributes specified)

Usage:
```c#
IContainer container = new Container();
container.RegisterType<SqlDataRepository>();
container.RegisterType<ServiceDataRepository>();
var srv1 = container.CreateInstance<ServiceDataRepository>();
```

### Load of external assembly type

[Load type using interface](https://github.com/hack2root/build/blob/master/Examples/AssemblyLoader/Program.cs)

### Classes

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

## Donate

Please, feel free to donate me [5$](https://www.paypal.me/experimentalworld/5) to expand project development (wiki, samples, etc.)