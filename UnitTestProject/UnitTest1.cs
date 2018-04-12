using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet1
    {
        [TestClass]
        public class UnitTest1
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
            public void TestSet1_Method1()
            {
                //TestSet1
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet1_Method2()
            {
                //TestSet1
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet1_Method3()
            {
                //TestSet1
                TestSet1_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet1_Method4()
            {
                //TestSet1
                TestSet1_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet1_Method5()
            {
                //TestSet1
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet1_Method6()
            {
                //TestSet1
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
