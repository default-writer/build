using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet4
    {
        [TestClass]
        public class UnitTest4
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
            public void TestSet4_Method1()
            {
                //TestSet4
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet4_Method2()
            {
                //TestSet4
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet4_Method3()
            {
                //TestSet4
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet4_Method4()
            {
                //TestSet4
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet4_Method5()
            {
                //TestSet4
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet4_Method6()
            {
                //TestSet4
                var srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                var srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
