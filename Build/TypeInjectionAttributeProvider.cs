using System;
using System.Reflection;

namespace Build
{
    public class TypeInjectionAttributeProvider : ITypeInjectionAttributeProvider
    {
        public virtual IInjectionAttribute GetAttribute(Module p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(Assembly p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(ParameterInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(PropertyInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(MethodInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(ConstructorInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(MemberInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
        public virtual IInjectionAttribute GetAttribute(ICustomAttributeProvider p, Type type, bool inherit = false) =>
            p.GetAttribute<InjectionAttribute>(type);
    }
}