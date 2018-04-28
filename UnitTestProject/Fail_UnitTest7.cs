using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet7
    {
        [TestClass]
        public class UnitTest7
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet7_Method1()
            {
                //Fail_TestSet7
                Assert.ThrowsException<TypeRegistrationException>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet7_Method2()
            {
                Assert.ThrowsException<TypeInstantiationException>(() => commonPersonContainer.CreateInstance<ServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet7_Method3()
            {
                //Fail_TestSet7
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                var serviceDataRepository = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNull(serviceDataRepository.Repository);
            }
            [TestMethod]
            public void Fail_TestSet7_Method4()
            {
                //Fail_TestSet7
                commonPersonContainer.RegisterType<OtherRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                var serviceDataRepository = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(serviceDataRepository.Repository);
            }
            [TestMethod]
            public void Fail_TestSet7_Method5()
            {
                //Fail_TestSet7
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                bool noException = false;
                try
                {
                    commonPersonContainer.RegisterType<ServiceDataRepository>();
                    noException = true;
                }
                catch
                {
                }
                Assert.IsTrue(noException);
            }
        }
    }
}
