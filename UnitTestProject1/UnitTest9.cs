using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet9
    {
        [TestClass]
        public class UnitTest9
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
            public void TestSet9_Method1()
            {
                //TestSet9
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet9_Method2()
            {
                //TestSet9
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet9_Method3()
            {
                //TestSet9
                TestSet9_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet9_Method4()
            {
                //TestSet9
                TestSet9_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet9_Method5()
            {
                //TestSet9
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet9_Method6()
            {
                //TestSet9
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
