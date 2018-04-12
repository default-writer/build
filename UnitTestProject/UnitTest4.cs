using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet4
    {
        [TestClass]
        public class UnitTest4
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
            public void TestSet4_Method1()
            {
                //TestSet4
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet4_Method2()
            {
                //TestSet4
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet4_Method3()
            {
                //TestSet4
                TestSet4_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet4_Method4()
            {
                //TestSet4
                TestSet4_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet4_Method5()
            {
                //TestSet4
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet4_Method6()
            {
                //TestSet4
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
