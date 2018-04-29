using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet12
    {
        [TestClass]
        public class UnitTest12
        {
            IContainer container;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();
            }
            [TestMethod]
            public void TestSet12_Method1()
            {
                //TestSet12
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet12_Method2()
            {
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                //TestSet12
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet12_Method3()
            {
                //TestSet12
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet12_Method4()
            {
                //TestSet12
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet12_Method5()
            {
                //TestSet12
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet12_Method6()
            {
                //TestSet12
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
