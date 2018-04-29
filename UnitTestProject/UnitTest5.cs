using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet5
    {
        [TestClass]
        public class UnitTest5
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
            public void TestSet5_Method1()
            {
                //TestSet5
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet5_Method2()
            {
                //TestSet5
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet5_Method3()
            {
                //TestSet5
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet5_Method4()
            {
                //TestSet5
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet5_Method5()
            {
                //TestSet5
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet5_Method6()
            {
                //TestSet5
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}