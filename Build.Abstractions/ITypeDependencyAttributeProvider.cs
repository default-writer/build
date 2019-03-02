using System;
using System.Reflection;

namespace Build
{
    public interface ITypeDependencyAttributeProvider
    {
        IDependencyAttribute GetAttribute(Module p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(Assembly p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(ParameterInfo p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(PropertyInfo p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(MethodInfo p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(ConstructorInfo p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(MemberInfo p, Type type, bool inherit = false);
        IDependencyAttribute GetAttribute(ICustomAttributeProvider p, Type type, bool inherit = false);
    }
}