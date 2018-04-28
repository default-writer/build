using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet3
    {
        [TestClass]
        public class UnitTest3
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet3_Method1()
            {
                //Fail_TestSet3
                Assert.ThrowsException<TypeRegistrationException>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method2()
            {
                //Fail_TestSet3
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method3()
            {
                //Fail_TestSet3
                Assert.ThrowsException<TypeFilterException>(() => commonPersonContainer.CreateInstance<OtherRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method4()
            {
                //Fail_TestSet3
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<ServiceDataRepository>());
            }
        }
    }
}
