using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    namespace TestSet7
    {
        [TestClass]
        public class UnitTest7
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
            public void TestSet7_Method1()
            {
                //TestSet7
                Assert.IsNotNull(srv1);
            }
            [TestMethod]
            public void TestSet7_Method2()
            {
                //TestSet7
                Assert.IsNotNull(srv2);
            }
            [TestMethod]
            public void TestSet7_Method3()
            {
                //TestSet7
                TestSet7_Method1();
                Assert.IsNotNull(srv1.Repository);
            }
            [TestMethod]
            public void TestSet7_Method4()
            {
                //TestSet7
                TestSet7_Method2();
                Assert.IsNotNull(srv2.Repository);
            }
            [TestMethod]
            public void TestSet7_Method5()
            {
                //TestSet7
                Assert.AreNotEqual(srv1, srv2);
            }
            [TestMethod]
            public void TestSet7_Method6()
            {
                //TestSet7
                Assert.AreNotEqual(srv1.Repository, srv2.Repository);
            }
        }
    }
}
