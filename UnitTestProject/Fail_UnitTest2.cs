using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet2
    {
        [TestClass]
        public class UnitTest2
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet2_Method1()
            {
                //Fail_TestSet2
                commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method2()
            {
                //Fail_TestSet2
                commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => commonPersonContainer.CreateInstance<IPersonRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method3()
            {
                //Fail_TestSet2
                Assert.ThrowsException<TypeRegistrationException>(() => commonPersonContainer.RegisterType<ServiceDataRepository>());
            }
        }
    }
}
