using System;
using System.Reflection;

namespace Build.Generics
{
    public interface ITypeDependencyAttributeProvider<T, U> : IRuntimeAttributeProvider<T, U>
        where T : Attribute, IDependencyAttribute
        where U : ICustomAttributeProvider
    {
    }
}