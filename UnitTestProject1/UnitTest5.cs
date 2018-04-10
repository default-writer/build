using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet5
    {
        [TestClass]
        public class UnitTest5
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
            public void TestSet5_Method1()
            {
                //TestSet5
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet5_Method2()
            {
                //TestSet5
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet5_Method3()
            {
                //TestSet5
                TestSet5_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet5_Method4()
            {
                //TestSet5
                TestSet5_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet5_Method5()
            {
                //TestSet5
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet5_Method6()
            {
                //TestSet5
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}