using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet3
    {
        [TestClass]
        public class UnitTest3
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet3_Method1()
            {
                //Fail_TestSet3
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method2()
            {
                //Fail_TestSet3
                container.RegisterType<ServiceDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method3()
            {
                //Fail_TestSet3
                Assert.ThrowsException<TypeFilterException>(() => container.CreateInstance<OtherRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method4()
            {
                //Fail_TestSet3
                container.RegisterType<ServiceDataRepository>();
                Assert.IsNotNull(container.CreateInstance<ServiceDataRepository>());
            }
        }
    }
}
