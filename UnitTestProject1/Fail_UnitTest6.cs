using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet6
    {
        [TestClass]
        public class UnitTest6
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet6_Method1()
            {
                //Fail_TestSet6
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<Exception>(() => commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet6_Method2()
            {
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<Exception>(() => commonPersonContainer.CreateInstance<ServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet6_Method3()
            {
                //Fail_TestSet6
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<ServiceDataRepository>());
            }
        }
    }
}
