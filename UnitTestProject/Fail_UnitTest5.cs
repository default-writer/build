using Build;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    namespace Fail_TestSet5
    {
        [TestClass]
        public class UnitTest5
        {
            IContainer commonPersonContainer;

            [TestInitialize]
            public void Initialize()
            {
                commonPersonContainer = new Container();
            }
            [TestMethod]
            public void Fail_TestSet5_Method1()
            {
                //Fail_TestSet5
                Assert.ThrowsException<Exception>(() => commonPersonContainer.CreateInstance<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method2()
            {
                //Fail_TestSet5
                Assert.ThrowsException<Exception>(() => commonPersonContainer.RegisterType<ServiceDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method3()
            {
                //Fail_TestSet5
                Assert.ThrowsException<TypeInjectionException>(() => commonPersonContainer.RegisterType<SqlDataRepository>());
            }
            [TestMethod]
            public void Fail_TestSet5_Method4()
            {
                //Fail_TestSet5
                commonPersonContainer.RegisterType<OtherRepository>();
                commonPersonContainer.RegisterType<NoSqlDataRepository>();
                Assert.ThrowsException<Build.TypeInitializationException>(() => commonPersonContainer.CreateInstance<NoSqlDataRepository>());
            }
        }
    }
}
