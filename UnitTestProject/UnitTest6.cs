using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet6
    {
        [TestClass]
        public class UnitTest6
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
            public void TestSet6_Method1()
            {
                //TestSet6
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet6_Method2()
            {
                //TestSet6
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet6_Method3()
            {
                //TestSet6
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet6_Method4()
            {
                //TestSet6
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet6_Method5()
            {
                //TestSet6
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet6_Method6()
            {
                //TestSet6
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
