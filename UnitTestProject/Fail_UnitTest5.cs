using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace Fail_TestSet5
    {
        [TestClass]
        public class UnitTest5
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
            }
            [TestMethod]
            public void Fail_TestSet5_Method1()
            {
                //Fail_TestSet5
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method2()
            {
                //Fail_TestSet5
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<ServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method3()
            {
                //Fail_TestSet5
                Assert.ThrowsException<TypeRegistrationException>(() => container.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method4()
            {
                //Fail_TestSet5
                container.RegisterType<OtherRepository>();
                container.RegisterType<NoSqlDataRepository>();
                Assert.ThrowsException<TypeInstantiationException>(() => container.CreateInstance<NoSqlDataRepository>());
            }
        }
    }
}
