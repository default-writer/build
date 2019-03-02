using System;
using System.Reflection;

namespace Build
{
    public interface ITypeInjectionAttributeProvider
    {
        IInjectionAttribute GetAttribute(Module p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(Assembly p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(ParameterInfo p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(PropertyInfo p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(MethodInfo p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(ConstructorInfo p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(MemberInfo p, Type type, bool inherit = false);
        IInjectionAttribute GetAttribute(ICustomAttributeProvider p, Type type, bool inherit = false);
    }
}