using System;
using System.Reflection;

namespace Build
{
    public class TypeDependencyAttributeProvider : ITypeDependencyAttributeProvider
    {
        public virtual IDependencyAttribute GetAttribute(Module p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(Assembly p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(ParameterInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(PropertyInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(MethodInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(ConstructorInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(MemberInfo p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
        public virtual IDependencyAttribute GetAttribute(ICustomAttributeProvider p, Type type, bool inherit = false) =>
            p.GetAttribute<DependencyAttribute>(type);
    }
}