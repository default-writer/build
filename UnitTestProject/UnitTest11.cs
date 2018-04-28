using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet11
    {
        [TestClass]
        public class UnitTest11
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();
            }
            [TestMethod]
            public void TestSet11_Method1()
            {
                //TestSet11
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet11_Method2()
            {
                //TestSet11
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet11_Method3()
            {
                //TestSet11
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet11_Method4()
            {
                //TestSet11
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet11_Method5()
            {
                //TestSet11
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet11_Method6()
            {
                //TestSet11
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
