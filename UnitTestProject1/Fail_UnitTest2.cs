using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet2
    {
        [TestClass]
        public class UnitTest2
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet2_Method1()
            {
                //Fail_TestSet2
                commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method2()
            {
                //Fail_TestSet2
                commonPersonContainer.RegisterType<SqlDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<IPersonRepository>());
            }
            [TestMethod]
            public void Fail_TestSet2_Method3()
            {
                //Fail_TestSet2
                Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<ServiceDataRepository>());
            }

            //[TestMethod]
            //public void Fail_TestSet2_Method4()
            //{
            //    //Fail_TestSet2
            //    Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<Int32>());
            //}
            //[TestMethod]
            //public void Fail_TestSet2_Method5()
            //{
            //    //Fail_TestSet2
            //    Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<IPersonRepository>());
            //}
            //[TestMethod]
            //public void Fail_TestSet2_Method6()
            //{
            //    //Fail_TestSet2
            //    commonPersonContainer.RegisterType<ServiceDataRepository>();
            //    Assert.IsNull(commonPersonContainer.CreateInstance<IPersonRepository>());
            //}
        }
    }
}