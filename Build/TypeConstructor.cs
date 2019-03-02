using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build
{

    public sealed class TypeConstructor : ITypeConstructor
    {
        public IEnumerable<ITypeDependencyObject> GetDependencyObjects(ITypeActivator runtimeTypeActivator, Type type, bool defaultTypeInstantiation, ITypeDependencyAttributeProvider typeDependencyAttributeProvider = null, ITypeInjectionAttributeProvider typeInjectionAttributeProvider = null)
        {
            var dependencyObjects = new List<ITypeDependencyObject>();
            var constructors = type.GetConstructors();
            if (constructors.Length == 0 && typeof(ValueType).IsAssignableFrom(type))
            {
                constructors = ((TypeInfo)type).DeclaredConstructors.ToArray();
                if (constructors.Length == 0)
                    constructors = ((TypeInfo)type.BaseType).DeclaredConstructors.ToArray();
            }
            foreach (var constructorInfo in constructors)
            {
                var dependencyAttribute = typeDependencyAttributeProvider.GetDependencyAttribute<DependencyAttribute>(type, constructorInfo);
                var injectionObjects = constructorInfo.GetParameters().Select(p => new TypeInjectionObject(runtimeTypeActivator, typeInjectionAttributeProvider.GetInjectionAttribute<InjectionAttribute>(p.ParameterType, p), p.ParameterType, defaultTypeInstantiation));
                dependencyObjects.Add(new TypeDependencyObject(runtimeTypeActivator, dependencyAttribute, injectionObjects, type, defaultTypeInstantiation));
            }
            return dependencyObjects;
        }

    }

    public static class TypeAttributeProviderExtensions
    {
        /*
        IRuntimeAttributeProvider<T, Module>,
        IRuntimeAttributeProvider<T, Assembly>,
        IRuntimeAttributeProvider<T, ParameterInfo>,
        IRuntimeAttributeProvider<T, PropertyInfo>,
        IRuntimeAttributeProvider<T, MethodInfo>,
        IRuntimeAttributeProvider<T, ConstructorInfo>,
        IRuntimeAttributeProvider<T, MemberInfo>,
        IRuntimeAttributeProvider<T, ICustomAttributeProvider>
         */
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, Module constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, Assembly constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, ParameterInfo constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, PropertyInfo constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, MethodInfo constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, ConstructorInfo constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, MemberInfo constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IDependencyAttribute GetDependencyAttribute<T>(this ITypeDependencyAttributeProvider typeDependencyAttributeProvider, Type type, ICustomAttributeProvider constructorInfo) where T : Attribute, IDependencyAttribute => typeDependencyAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<T>(type);
        public static IInjectionAttribute GetInjectionAttribute<T>(this ITypeInjectionAttributeProvider typeInjectionAttributeProvider, Type type, ParameterInfo constructorInfo) where T: Attribute, IInjectionAttribute => typeInjectionAttributeProvider?.GetAttribute(constructorInfo, type) ?? constructorInfo.GetAttribute<InjectionAttribute>(type);
    }
}