using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet12
    {
        [TestClass]
        public class UnitTest12
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
            public void TestSet12_Method1()
            {
                //TestSet12
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet12_Method2()
            {
                //TestSet12
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet12_Method3()
            {
                //TestSet12
                TestSet12_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet12_Method4()
            {
                //TestSet12
                TestSet12_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet12_Method5()
            {
                //TestSet12
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet12_Method6()
            {
                //TestSet12
                Assert.AreEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
