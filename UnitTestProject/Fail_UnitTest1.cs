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
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet1_Method1()
            {
                //Fail_TestSet1
                bool throwsException = false;
                try
                {
                    container.RegisterType<SqlDataRepository>();
                }
                catch (Exception)
                {
                    throwsException = true;
                }
                Assert.AreNotEqual(throwsException, true);
            }
            [TestMethod]
            public void Fail_TestSet1_Method2()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<PrivateConstructorServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method3()
            {
                //Fail_TestSet1
                container.RegisterType<ServiceDataRepository>();
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNull(srv1.Repository);
            }
            [TestMethod]
            public void Fail_TestSet1_Method4()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeFilterException>(() => container.RegisterType<int>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method5()
            {
                //Fail_TestSet1
                Assert.ThrowsException<TypeFilterException>(() => container.RegisterType<IPersonRepository>());
            }
            [TestMethod]
            public void Fail_TestSet1_Method6()
            {
                //Fail_TestSet1
                container.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
            }
        }
    }
}
