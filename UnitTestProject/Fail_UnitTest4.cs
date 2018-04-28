using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet4
    {
        [TestClass]
        public class UnitTest4
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet4_Method1()
            {
                //Fail_TestSet4
                //commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method2()
            {
                //Fail_TestSet4
                //commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => commonPersonContainer.CreateInstance<OtherRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method3()
            {
                //Fail_TestSet4
                Assert.ThrowsException<TypeRegistrationException>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method4()
            {
                //Fail_TestSet4
                Assert.ThrowsException<TypeRegistrationException>(() => commonPersonContainer.RegisterType<ServiceDataRepository>());
            }
        }
    }
}
