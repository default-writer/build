using System;
using System.Reflection;

namespace Build.Generics
{
    public interface ITypeInjectionAttributeProvider<T, U> : IRuntimeAttributeProvider<T, U>
        where T : Attribute, IInjectionAttribute
        where U : ICustomAttributeProvider
    {
    }
}