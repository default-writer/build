using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet11
    {
        [TestClass]
        public class UnitTest11
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
            public void TestSet11_Method1()
            {
                //TestSet11
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet11_Method2()
            {
                //TestSet11
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet11_Method3()
            {
                //TestSet11
                TestSet11_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet11_Method4()
            {
                //TestSet11
                TestSet11_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet11_Method5()
            {
                //TestSet11
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet11_Method6()
            {
                //TestSet11
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
