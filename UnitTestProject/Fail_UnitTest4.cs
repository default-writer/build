using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet4
    {
        [TestClass]
        public class UnitTest4
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet4_Method1()
            {
                //Fail_TestSet4
                //container.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method2()
            {
                //Fail_TestSet4
                //container.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<OtherRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method3()
            {
                //Fail_TestSet4
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet4_Method4()
            {
                //Fail_TestSet4
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
            }
        }
    }
}
