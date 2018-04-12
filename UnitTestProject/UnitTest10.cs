using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet10
    {
        [TestClass]
        public class UnitTest10
        {
            ServiceDataRepository srv1, srv2;

            [TestInitialize]
            public void Initialize()
            {
                IContainer commonPersonContainer = new Container();
                commonPersonContainer.RegisterType<SqlDataRepository>();
                commonPersonContainer.RegisterType<ServiceDataRepository>();

                srv1 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
                srv2 = commonPersonContainer.CreateInstance<ServiceDataRepository>();
            }
            [TestMethod]
            public void TestSet10_Method1()
            {
                //TestSet10
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet10_Method2()
            {
                //TestSet10
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet10_Method3()
            {
                //TestSet10
                TestSet10_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet10_Method4()
            {
                //TestSet10
                TestSet10_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet10_Method5()
            {
                //TestSet10
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet10_Method6()
            {
                //TestSet10
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
