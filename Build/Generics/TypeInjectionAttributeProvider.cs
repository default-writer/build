
using System;
using System.Reflection;

namespace Build.Generics
{ 
    public class TypeInjectionAttributeProvider<T> : RuntimeAttributeProvider<T>, 
        ITypeInjectionAttributeProvider<T, PropertyInfo> 
        where T : Attribute, IInjectionAttribute
    {
    }
}