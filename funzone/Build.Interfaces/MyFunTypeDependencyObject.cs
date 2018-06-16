using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Build.Interfaces
{
    class MyFunTypeDependencyObject : TypeObject, ITypeDependencyObject
    {
        public MyFunTypeDependencyObject(MethodInfo constructorInfo, Type runtimeType, bool defaultTypeInstantiation) : base(GetDependencyAttribute(constructorInfo, runtimeType), runtimeType, defaultTypeInstantiation)
        {
            var dependencyAttribute = (MyFunDependencyAttribute)RuntimeAttribute;
            DependencyAttribute = dependencyAttribute;
            InjectionObjects = new List<ITypeInjectionObject>(constructorInfo.GetParameters().Select(p => new MyFunTypeInjectionObject(p, defaultTypeInstantiation)));
            TypeParameters = InjectionObjects.Select(p => p.RuntimeType.TypeFullName);
            TypeFullNameWithParameters = Format.GetConstructorWithParameters(TypeFullName, TypeParameters);
        }

        /// <summary>
        /// Dependency attribute
        /// </summary>
        public IDependencyAttribute DependencyAttribute { get; }

        /// <summary>
        /// Enumerates type parameters
        /// </summary>
        public IEnumerable<ITypeInjectionObject> InjectionObjects { get; }

        /// <summary>
        /// Type full name with parameters
        /// </summary>
        public string TypeFullNameWithParameters { get; }

        /// <summary>
        /// Type parameters full name
        /// </summary>
        public IEnumerable<string> TypeParameters { get; }

        /// <summary>
        /// Gets the dependency attribute.(ConstructorInfo's DeclaringType)
        /// </summary>
        /// <param name="constructorInfo">The constructor.</param>
        /// <param name="runtimeType">Type to be instantiated</param>
        /// <returns>Returns custom dependency attrubute</returns>
        static MyFunDependencyAttribute GetDependencyAttribute(MethodInfo constructorInfo, Type runtimeType) => constructorInfo.GetCustomAttribute<MyFunDependencyAttribute>() ?? new MyFunDependencyAttribute(runtimeType, RuntimeInstance.CreateInstance);
    }
}