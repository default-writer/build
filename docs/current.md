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
