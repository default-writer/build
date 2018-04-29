using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet13
    {
        [TestClass]
        public class UnitTest13
        {
            Container container;
            ServiceDataRepository srv1, srv2;

            [TestInitialize]
            public void Initialize()
            {
                container = new Container();
                container.RegisterType<SqlDataRepository>();
                container.RegisterType<ServiceDataRepository>();

                //SqlDataRepository sql = container.CreateInstance<SqlDataRepository>();
                srv1 = container.CreateInstance<ServiceDataRepository>();
                srv2 = container.CreateInstance<ServiceDataRepository>();
            }

            [TestMethod]
            public void TestSet13_Method1()
            {
                //TestSet13
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet13_Method2()
            {
                //TestSet13
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet13_Method3()
            {
                //TestSet13
                TestSet13_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet13_Method4()
            {
                //TestSet13
                TestSet13_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet13_Method5()
            {
                //TestSet13
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet13_Method6()
            {
                //TestSet13
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
