Welcome to the build wiki!

# .NET Core 2.1 simple Dependency Injection micro framework

## Features

* Declarative metadata attribute driven initialization
* Lazy type resolution and initialization
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

## Links

[Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1)

[Inversion of Control Containers and the Dependency Injection pattern](https://www.martinfowler.com/articles/injection.html)