using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet7
    {
        [TestClass]
        public class UnitTest7
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
            public void TestSet7_Method1()
            {
                //TestSet7
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet7_Method2()
            {
                //TestSet7
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet7_Method3()
            {
                //TestSet7
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet7_Method4()
            {
                //TestSet7
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet7_Method5()
            {
                //TestSet7
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet7_Method6()
            {
                //TestSet7
                var srv1 = container.CreateInstance<ServiceDataRepository>();
                var srv2 = container.CreateInstance<ServiceDataRepository>();
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
