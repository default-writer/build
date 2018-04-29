using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet7
    {
        [TestClass]
        public class UnitTest7
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet7_Method1()
            {
                //Fail_TestSet7
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet7_Method2()
            {
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<ServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet7_Method3()
            {
                //Fail_TestSet7
                container.RegisterType<ServiceDataRepository>();
                var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNull(serviceDataRepository.Repository);
            }
            [TestMethod]
            public void Fail_TestSet7_Method4()
            {
                //Fail_TestSet7
                container.RegisterType<OtherRepository>();
                container.RegisterType<ServiceDataRepository>();
                var serviceDataRepository = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(serviceDataRepository.Repository);
            }
            [TestMethod]
            public void Fail_TestSet7_Method5()
            {
                //Fail_TestSet7
                container.RegisterType<ServiceDataRepository>();
                bool noException = false;
                try
                {
                    container.RegisterType<ServiceDataRepository>();
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
