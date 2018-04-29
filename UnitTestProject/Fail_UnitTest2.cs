using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet2
    {
        [TestClass]
        public class UnitTest2
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet2_Method1()
            {
                //Fail_TestSet2
                container.RegisterType<SqlDataRepository>();
                Assert.IsNotNull(container.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method2()
            {
                //Fail_TestSet2
                container.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method3()
            {
                //Fail_TestSet2
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
            }
        }
    }
}
