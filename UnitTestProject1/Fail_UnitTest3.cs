using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet3
    {
        [TestClass]
        public class UnitTest3
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet3_Method1()
            {
                //Fail_TestSet3
                Assert.ThrowsException<Exception>(()=> commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method2()
            {
                //Fail_TestSet3
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.IsNull(commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet3_Method3()
            {
                //Fail_TestSet3
                commonPersonContainer.RegisterType<ServiceDataRepository>();
                Assert.IsNotNull(commonPersonContainer.CreateInstance<ServiceDataRepository>());
            }

            //[TestMethod]
            //public void Fail_TestSet3_Method4()
            //{
            //    //Fail_TestSet3
            //    Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<Int32>());
            //}
            //[TestMethod]
            //public void Fail_TestSet3_Method5()
            //{
            //    //Fail_TestSet3
            //    Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<IPersonRepository>());
            //}
            //[TestMethod]
            //public void Fail_TestSet3_Method6()
            //{
            //    //Fail_TestSet3
            //    commonPersonContainer.RegisterType<ServiceDataRepository>();
            //    Assert.IsNull(commonPersonContainer.CreateInstance<IPersonRepository>());
            //}
        }
    }
}