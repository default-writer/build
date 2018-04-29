using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet6
    {
        [TestClass]
        public class UnitTest6
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet6_Method1()
            {
                //Fail_TestSet6
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet6_Method2()
            {
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
            }
             [TestMethod]
            public void Fail_TestSet6_Method3()
            {
                container.RegisterType<PublicDataRepository>();
                Assert.IsNotNull(container.CreateInstance<PublicDataRepository>());
            }
        }
    }
}
