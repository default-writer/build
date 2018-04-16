using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet1
    {
        [TestClass]
        public class UnitTest1
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet1_Method1()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeInjectionException>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method2()
            {
                //Fail_TestSet1
                Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<PrivateConstructorServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method3()
            {
                //Fail_TestSet1
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                ServiceDataRepository srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNull(srv1.Repository);
            }
            [TestMethod]
            public void Fail_TestSet1_Method4()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeFilterException>(() => commonPersonContainer.RegisterType<int>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method5()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeFilterException>(() => commonPersonContainer.RegisterType<IPersonRepository>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method6()
            {
                //Fail_TestSet1
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<Exception>(() => commonPersonContainer.CreateInstance<IPersonRepository>());
            }
        }
    }
}
