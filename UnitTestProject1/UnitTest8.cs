using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet8
    {
        [TestClass]
        public class UnitTest8
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
            public void TestSet8_Method1()
            {
                //TestSet8
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet8_Method2()
            {
                //TestSet8
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet8_Method3()
            {
                //TestSet8
                TestSet8_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet8_Method4()
            {
                //TestSet8
                TestSet8_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet8_Method5()
            {
                //TestSet8
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet8_Method6()
            {
                //TestSet8
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
