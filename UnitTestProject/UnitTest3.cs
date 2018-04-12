using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet3
    {
        [TestClass]
        public class UnitTest3
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
            public void TestSet3_Method1()
            {
                //TestSet3
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet3_Method2()
            {
                //TestSet3
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet3_Method3()
            {
                //TestSet3
                TestSet3_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet3_Method4()
            {
                //TestSet3
                TestSet3_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet3_Method5()
            {
                //TestSet3
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet3_Method6()
            {
                //TestSet3
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
