
# CodeProject

## Description 

Original projects source codes for Presentation, Businesss, Data contains unmodified source code from the original article.

### Tier tight coupling 

* Presentation linked to Business
* Business linked to Data

### Tier decoupling

* Extracted API interfaces from Business, Data
* PresentationCore linked to BusinessCore.API, DataCore.API (interface-only assemblies)
* BuisinessCore linked to BusinessCore,API, DataCore.API (interface-only assemblies)
* DataCore linked to DataCore.API (interface-only assemblies)

## Links

[Dependency Injection using Unity: Resolve dependency of dependencies](https://www.codeproject.com/Articles/1234518/Dependency-Injection-using-Unity-Resolve-dependenc)
