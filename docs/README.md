Welcome to the build wiki!

# .NET Core 2.1 Dependency Injection framework

[![build-core MyGet Build Status](https://www.myget.org/BuildSource/Badge/build-core?identifier=131b1afa-1799-4a6a-815a-13115ffae809)](https://www.myget.org/) [![VSTS](https://hack2root.visualstudio.com/_apis/public/build/definitions/6ec45376-8260-482c-906f-4bf4d4e8e451/4/badge)](https://hack2root.visualstudio.com/build) [![CircleCI](https://img.shields.io/circleci/project/github/hack2root/build.svg)](https://circleci.com/gh/hack2root/build) [![Build status](https://ci.appveyor.com/api/projects/status/k9d5256ualhy2skp/branch/master?svg=true)](https://ci.appveyor.com/project/hack2root/build/branch/master) [![Amazon AWS](https://codebuild.us-east-2.amazonaws.com/badges?uuid=eyJlbmNyeXB0ZWREYXRhIjoiYU5ZMHd1WVdNdWZzdzlrTS96VEhJMnEvSFlQK2UxelZhWWMwa3hYclVmcjNGM05IaW5xcFdqY3JnNVJxUitnbkxCRWVPOGpYa1REU1czNmhNdUFmZzVjPSIsIml2UGFyYW1ldGVyU3BlYyI6IkUxaWd1YnRBUGpxTHBNY0MiLCJtYXRlcmlhbFNldFNlcmlhbCI6MX0%3D&branch=master)](https://us-east-2.console.aws.amazon.com/codebuild/home?region=us-east-2#/projects/build/view) [![Travis CI](https://travis-ci.org/hack2root/build.svg?branch=master)](https://travis-ci.org/hack2root/build) [![CodeFactor](https://www.codefactor.io/repository/github/hack2root/build/badge)](https://www.codefactor.io/repository/github/hack2root/build)

## Maintainability

[![build](https://sonarcloud.io/api/project_badges/measure?project=build-core&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=build-core) [![build](https://sonarcloud.io/api/project_badges/measure?project=build-core&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=build-core) [![build](https://sonarcloud.io/api/project_badges/measure?project=build-core&metric=alert_status)](https://sonarcloud.io/dashboard?id=build-core) [![build](https://sonarcloud.io/api/project_badges/measure?project=build-core&metric=ncloc)](https://sonarcloud.io/dashboard?id=build-core) [![build](https://sonarcloud.io/api/project_badges/measure?project=build-core&metric=coverage)](https://sonarcloud.io/dashboard?id=build-core) [![Coverage Status](https://coveralls.io/repos/github/hack2root/build/badge.svg?branch=master)](https://coveralls.io/github/hack2root/build?branch=master)

## Community

Welcome to [#build](https://join.slack.com/t/build-core/shared_invite/enQtMzY3NjQ2Nzc5MzAyLWE3MmFkOWFlNmY4NWJlMjU2YmVkNDNmNTI2YjUwMGIzNWY1MjAyNzU0NTUzNDU4MmViOGQxYTkwZDkwNTBjMTI) [#slack](https://join.slack.com/t/build-core/shared_invite/enQtMzY3NjQ2Nzc5MzAyLWE3MmFkOWFlNmY4NWJlMjU2YmVkNDNmNTI2YjUwMGIzNWY1MjAyNzU0NTUzNDU4MmViOGQxYTkwZDkwNTBjMTI)

[![CodeFactor](https://www.codefactor.io/repository/github/hack2root/build/badge)](https://www.codefactor.io/repository/github/hack2root/build) [![NuGet version](https://img.shields.io/nuget/v/dependency_injection_build.svg)](https://www.nuget.org/packages/dependency_injection_build) [![NuGet version](https://img.shields.io/nuget/v/Build.DependencyInjection.svg)](https://www.nuget.org/packages/Build.DependencyInjection) [![NuGet downloads](https://img.shields.io/nuget/dt/dependency_injection_build.svg)](https://www.nuget.org/packages/dependency_injection_build) [![NuGet downloads](https://img.shields.io/nuget/dt/Build.DependencyInjection.svg)](https://www.nuget.org/packages/Build.DependencyInjection) [![Discord](https://img.shields.io/discord/446426366616010763.svg)](https://discord.gg/uqrmTY4) 

## Channels

[#slack](https://join.slack.com/t/build-core/shared_invite/enQtMzY3NjQ2Nzc5MzAyLWE3MmFkOWFlNmY4NWJlMjU2YmVkNDNmNTI2YjUwMGIzNWY1MjAyNzU0NTUzNDU4MmViOGQxYTkwZDkwNTBjMTI)

## Repository

[build](https://github.com/hack2root/build)

## Docs 

- [Build](https://github.com/hack2root/build/wiki)

## v1.0.0.19

- Added API functions
- All CreateInstance, GetInstance, RegisterType now have the same semantics
- Exposed public Container API to IContainer

```c#
Container.CreateInstance(string)
Container.CreateInstance(string, object[])
Container.CreateInstance(string, string[])
Container.CreateInstance(string, System.Type[])
```

```c#
Container.CreateInstance(System.Type) 
Container.CreateInstance(System.Type, object[])
Container.CreateInstance(System.Type, string[])
Container.CreateInstance(System.Type, System.Type[])
```

```c#
Container.CreateInstance<T>() 
Container.CreateInstance<T>(object[]) 
Container.CreateInstance<T>(string[]) 
Container.CreateInstance<T>(System.Type[])
```

```c#
Container.GetInstance(string) 
Container.GetInstance(string, object[]) 
Container.GetInstance(string, string[]) 
Container.GetInstance(string, System.Type[])
```

```c#
Container.GetInstance(System.Type)
Container.GetInstance(System.Type, object[])
Container.GetInstance(System.Type, string[])
Container.GetInstance(System.Type, System.Type[])
```

```c#
Container.RegisterType(string) 
Container.RegisterType(string, object[]) 
Container.RegisterType(string, string[]) 
Container.RegisterType(string, System.Type[])
```

```c#
Container.RegisterType(System.Type) 
Container.RegisterType(System.Type, object[]) 
Container.RegisterType(System.Type, string[]) 
Container.RegisterType(System.Type, System.Type[]) 
```

```c#
Container.RegisterType<T>()
Container.RegisterType<T>(object[])
Container.RegisterType<T>(string[])
Container.RegisterType<T>(System.Type[])
```

## v1.0.0.18
- Added API functions

## v1.0.0.17
- Added value types support

## v1.0.0.16

- Added ```TypeBuilderOptions``` class
- Added [sample](https://github.com/hack2root/build/tree/master/Build.Tests/Classes/PropertyTypeConstructor.cs) implementation of property-based type dependency injector

## v1.0.0.15

- Added [ASP.NET Core 2.1 middleware extensibility](https://github.com/hack2root/build/blob/master/aspnetcore/fundamentals/middleware/extensibility-third-party-container-build/sample/)
- Added ```System.Func<T>``` type initialization
- Added ```TypeBuilder.GetInstance(...)``` methods

## v1.0.0.14

- Added type registration parameters
- Added support for generic parameter types
- Use standart type definiton (Type.ToString())

Example:
```c#
//TestSet18
var container = new Container();
var type = new Type();
System.Func<Type> func = () => type;
container.RegisterType<Lazy<Type>>(func);
var factory = (IFactory<Type>)container.CreateInstance("Build.Tests.TestSet18.Lazy`1[Build.Tests.TestSet18.Type]");
Assert.Equal(type, factory.GetInstance());
```

Definition:
```c#
public interface IFactory<T>
{
    T GetInstance();
}

public class Lazy<T> : IFactory<T>
{
    public Lazy(Func<T> func) => Func = func;

    public Func<T> Func { get; }

    public T GetInstance() => Func();
}
```

## v1.0.0.13

- Added runtimes for .NET Standart 2.0, .NET Framework 4.5/4.5.1/4.5.2/4.6/4.6.1/4.6.2/4.7/4.7.1/4.7.2

## v1.0.0.12

- Added runtime default checks, to avoid System.MissingMethodException and System.Reflection.AmbiguousMatchException.

## v1.0.0.11

- Added automatic type attribute overwrite optional parameter to the build system
- By default, type runtime attributes overrides each other and optionally can be turned off. Exception will be trown.

Example:
```c#
[Interface]
interface IInterfaceRuleSet2
{
    [InterfaceDependency(RuntimeInstance.Singleton)]
    SqlDataRepository Rule(int repositoryId);
}

[Interface]
interface IInterfaceRuleSet2_Overwrite
{
    [InterfaceDependency(RuntimeInstance.Singleton)]
    SqlDataRepository Rule(int repositoryId);
}
```

## v1.0.0.10

- Added method CanRegisterParameter to ITypeFilter to control parameter registration

## v1.0.0.9

- Added public sealed classes TypeDependencyObject, TypeInjectionObject

## v1.0.0.8

- Added totally customizable type system
- Added ability to use interfaces as first-class objects

### Examples
#### Customizable type system

- You can use interfaces as first-class objects instead of classes fot typeholders attributes
- Using interfaces as first-class objects, you will be able to eliminate the need to inject attributes into existing code
- By implemeting type system interfaces, you will get a fully customzable type system, easily
- Registration is bound to interfaces only, which can have a several metods named "Rule" (it is evil, i knew it)
- Type is being instantiatied is actually a return parameter of "Rule" method
- Arguments passed to the constructor is simply arguments of that particular rule
- You definetly can break some more rules, just do not try to build inconsistent type system.

Definition:
```c#
interface IInterfaceRuleSet
{
    Type1 Rule(Arg1 arg1, Arg2 arg2);
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
```

Usage:
```c#
var container = new Container(new InterfaceTypeConstructor(), new InterfaceTypeFilter(), new InterfaceTypeParser(), new InterfaceTypeResolver());
container.RegisterType<IInterfaceRuleSet>();
var type1 = container.CreateInstance("Build.Tests.TestSet21.Type1(Build.Tests.TestSet21.Arg1,Build.Tests.TestSet21.Arg2)");
Assert.NotNull(type1);
```



## v1.0.0.7

- Enable/disable automatic type resolution
- Enable/disable automatic type instantiation

### Examples
#### Automatic type resolution disabled

Parameter is set to true if automatic type resolution for reference types option enabled (does not throws exceptions for reference types contains type dependencies to non-registered types). If automatic type resolution for reference types is enabled, type will defaults to null if not resolved and no exception will be thrown.

Usage:

```c#
// Automatic type resolution disabled
// Instantiation reqires SqlDataRepository to be resolved
var container = new Container(false, true);
container.RegisterType<ServiceDataRepository>();
container.RegisterType<WebServiceDataRepository>();
Assert.Throws<TypeInstantiationException>(() => container.CreateInstance("Build.Tests.TestSet15.WebServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)"));
```

```c#
/// Automatic type resolution enabled
var container = new Container(true, false);
container.RegisterType<WebServiceDataRepository>();
var sql = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet15.WebServiceDataRepository(Build.Tests.TestSet15.SqlDataRepository)");
Assert.Equal(2019, ((SqlDataRepository)sql.Repository).PersonId);
```

Definition: 

```c#
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
```

#### Automatic type instantiation disabled

Parameter is set to true if automatic type instantiation for reference types option enabled (does not throws exceptions for reference types defaults to null). If automatic type instantiation for reference types is enabled, type will defaults to null if not resolved and no exception will be thrown.

Usage:
```c#
// Automatic type instantiation disabled
var container = new Container(true, false);
container.RegisterType<ServiceDataRepository2>();
Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository2>());
```

```c#
// Automatic type instantiation enabled
// ServiceDataRepository2 depends upon non existent Build.Tests.Fail_TestSet1.Other2
// and resolved to null
var container = new Container(false, true);
container.RegisterType<ServiceDataRepository2>();
var sql = container.CreateInstance<ServiceDataRepository2>();
Assert.Null(sql.Repository);
```

Definition: 

```c#
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
```

## v1.0.0.5

- Automatic parameters cleanup
- Registered ValueType instantiation
- RuntimeAliasedTypes,RuntimeNonAliasedTypes,RuntimeTypes & RuntimeTypeAliases properties
- Reset() function

### Examples

#### Automatic parameters cleanup

Usage:

```c#
var instance1 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)", 123);
var instance2 = (SqlDataRepository)container.CreateInstance("Build.Tests.TestSet3.SqlDataRepository(System.Int32)");
Assert.Equal(0, instance2.PersonId);
```

#### Registered ValueType instantiation

Usage:
```c#
var instance = (int)container.CreateInstance("System.Int32()");
Assert.True(instance == 0);
```

#### RuntimeAliasedTypes,RuntimeNonAliasedTypes,RuntimeTypes & RuntimeTypeAliases properties

Usage: 

```c#
var instances = container.RuntimeAliasedTypes.Select(p => container.CreateInstance(p));
Assert.True(instances.All(p => p != null));
```
```c#
var instances = container.RuntimeNonAliasedTypes.Select(p => container.CreateInstance(p));
Assert.True(instances.All(p => p != null));
```
```c#
var instances = container.RuntimeTypes.Select(p => container.CreateInstance(p));
Assert.True(instances.All(p => p != null));
```
```c#
var instances = container.RuntimeTypeAliases.Select(p => container.CreateInstance(p));
Assert.True(instances.All(p => p != null));
```

#### Reset() function

Usage: 

```c#
container.RegisterType<SqlDataRepository>();
container.RegisterType<ServiceDataRepository>();
bool exception = false;
try
{
    container.Reset();
}
catch (Exception)
{
    exception = true;
}
Assert.False(exception);
```

## v1.0.0.4

- Removed public type specificator requirement

### Examples

#### Private type resolution (removed public type requirement)

Usage:

```c#
container.RegisterType<PrivateSqlDataRepository>();
var srv1 = container.CreateInstance("Build.Tests.TestSet1.PrivateSqlDataRepository");
Assert.NotNull(srv1);
```

Definition: 

```c#
class PrivateSqlDataRepository : IPersonRepository
{
    public PrivateSqlDataRepository()
    {
    }

    public PrivateSqlDataRepository(int personId)
    {
        PersonId = personId;
    }

    public int PersonId { get; }

    public Person GetPerson(int personId)
    {
        // get the data from SQL DB and return Person instance.
        return new Person(this);
    }
}
```

## v1.0.0.3

- Added support for automatic type resolution (type dependency resolution)

### Examples

#### Automatic type dependency resolution

Usage:

```c#
// ServiceDataRepository depends upon SqlDataRepository
container.RegisterType<ServiceDataRepository>();
var sql = (ServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.ServiceDataRepository(Build.Tests.TestSet16.SqlDataRepository)");
Assert.NotNull(sql.Repository);
```

Definition: 

```c#
public class SqlDataRepository : IPersonRepository
{
    [Dependency(RuntimeInstance.Singleton)]
    public SqlDataRepository(int repositoryId) => RepositoryId = repositoryId;

    public int RepositoryId { get; }

    public Person GetPerson(int personId)
    {
        // get the data from SQL DB and return Person instance.
        return new Person(this);
    }
}

public class ServiceDataRepository : IPersonRepository
{
    public ServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

    public ServiceDataRepository([Injection(typeof(SqlDataRepository), 2018)]IPersonRepository repository)
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
```

## v1.0.0.2

- Added support for multiple dependency injection attributes

### Examples

#### Constrctor parametrization

Usage:

```c#
var sql1 = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.ServiceDataRepository)");
Assert.Equal(2019, ((ServiceDataRepository)sql1.RepositoryA).RepositoryId);

var sql2 = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.IPersonRepository, Build.Tests.TestSet16.IPersonRepository)");
Assert.Equal(2020, ((ServiceDataRepository)sql2.RepositoryA).RepositoryId);

var sql3 = (WebServiceDataRepository)container.CreateInstance("Build.Tests.TestSet16.WebServiceDataRepository(Build.Tests.TestSet16.IPersonRepository, Build.Tests.TestSet16.IPersonRepository)");
Assert.Equal(2021, ((SqlDataRepository)sql3.RepositoryB).RepositoryId);
```

Definition:

```c#
public class WebServiceDataRepository : IPersonRepository
{
    public WebServiceDataRepository(int repositoryId) => RepositoryId = repositoryId;

    public WebServiceDataRepository([Injection(typeof(ServiceDataRepository), 2019)]IPersonRepository repository)
    {
        RepositoryA = repository;
    }

    public WebServiceDataRepository(
        [Injection("Build.Tests.TestSet16.ServiceDataRepository", 2020)]IPersonRepository repositoryA,
        [Injection("Build.Tests.TestSet16.SqlDataRepository", 2021)]IPersonRepository repositoryB)
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
```

## v1.0.0.1

### Major improvements

- Added support for default parameterless constructor with parameters injection using attributes

### Examples

#### Default constructor with parameters injection

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

## v1.0.0.0

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

### API

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

## Continious Integration

[CircleCI](https://circleci.com/gh/hack2root/build)
[TravisCI](https://travis-ci.org/hack2root/build)

## Academic papers:

[Dependency Injection for Programming by Optimization, Zoltan A. Kocsis and Jerry Swan](https://arxiv.org/pdf/1707.04016.pdf)

1. School of Mathematics, University of Manchester,
Oxford Road, Manchester M13 9PL, UK.
zoltan.kocsis@postgrad.manchester.ac.uk
2. Computer Science, University of York,
Deramore Lane, York, YO10 5GH, UK.

## Links

- [Explicit Dependencies Principle](https://deviq.com/explicit-dependencies-principle/)
- [Inversion of Control Containers and the Dependency Injection pattern](https://www.martinfowler.com/articles/injection.html)
- [Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1)
- [ASP.NET Core Middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index?view=aspnetcore-2.1&tabs=aspnetcore2x)
- [Middleware activation with a third-party container in ASP.NET Core.](https://docs.microsoft.com/aspnet/core/fundamentals/middleware/extensibility-third-party-container)
- [Simple Injector sample for ASP.NET](https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/middleware/extensibility-third-party-container/sample)

## Donate

[![Support via PayPal](https://www.paypalobjects.com/webstatic/mktg/Logo/pp-logo-200px.png)](https://www.paypal.me/experimentalworld/5)

Please, feel free to donate me [5$](https://www.paypal.me/experimentalworld/5) to expand project development (wiki, samples, etc.)
