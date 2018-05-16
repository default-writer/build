using System;
using Xunit;

namespace Build.Tests.TestSet0
{
    public class UnitTest
    {
        readonly IContainer container;

        public UnitTest()
        {
            container = new Container();
        }

        [Fact]
        public void TestSet0_Method1()
        {
            //TestSet1
            container.RegisterType<SqlDataRepository>();
            container.RegisterType<ServiceDataRepository>();
            bool exception = false;
            try
            {
                container.Reset();
            }
            catch (Exception)
            {
                exception = true;
            }
            Assert.False(exception);
        }
    }
}