using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet2
    {
        [TestClass]
        public class UnitTest2
        {
            ServiceDataRepository srv1, srv2;

            [TestInitialize]
            public void Initialize()
            {
                IContainer commonPersonContainer = new Container();
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();

                //SqlDataRepository sql = commonPersonContainer.CreateInstance<SqlDataRepository>();
                srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
            }
            [TestMethod]
            public void TestSet2_Method1()
            {
                //TestSet2
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet2_Method2()
            {
                //TestSet2
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet2_Method3()
            {
                //TestSet2
                TestSet2_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet2_Method4()
            {
                //TestSet2
                TestSet2_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet2_Method5()
            {
                //TestSet2
                Assert.AreEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet2_Method6()
            {
                //TestSet2
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
