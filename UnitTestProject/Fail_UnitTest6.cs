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
                Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet6_Method2()
            {
                Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<ServiceDataRepository>());
            }
             [TestMethod]
            public void Fail_TestSet6_Method3()
            {
                commonPersonContainer.RegisterType<PublicDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<PublicDataRepository>());
            }
        }
    }
}
