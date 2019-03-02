using System;
using System.Reflection;

namespace Build.Generics
{
    public class TypeDependencyAttributeProvider<T> : RuntimeAttributeProvider<T>, 
        ITypeDependencyAttributeProvider<T, ConstructorInfo> 
        where T : Attribute, IDependencyAttribute
    {
    }
}