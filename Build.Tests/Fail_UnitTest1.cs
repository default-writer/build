using System;
using Xunit;

namespace Build.Tests.Fail_TestSet1
{
    public class UnitTest
    {
        private IContainer container;

        public UnitTest() => container = new Container();

        [Fact]
        public void Fail_TestSet1_Method1()
        {
            //Fail_TestSet1
            bool throwsException = false;
            try
            {
                container.RegisterType<SqlDataRepository>();
            }
            catch (Exception)
            {
                throwsException = true;
            }
            Assert.False(throwsException);
        }

        [Fact]
        public void Fail_TestSet1_Method2()
        {
            //Fail_TestSet1
            Assert.Throws<TypeRegistrationException>(() => container.RegisterType<PrivateConstructorServiceDataRepository>());
        }

        [Fact]
        public void Fail_TestSet1_Method3()
        {
            //Fail_TestSet1
            container.RegisterType<ServiceDataRepository>();
            var srv1 = container.CreateInstance<ServiceDataRepository>();
            Assert.Null(srv1.Repository);
        }

        [Fact]
        public void Fail_TestSet1_Method4()
        {
            //Fail_TestSet1
            Assert.Throws<TypeFilterException>(() => container.RegisterType<int>());
        }

        [Fact]
        public void Fail_TestSet1_Method5()
        {
            //Fail_TestSet1
            Assert.Throws<TypeFilterException>(() => container.RegisterType<IPersonRepository>());
        }

        [Fact]
        public void Fail_TestSet1_Method6()
        {
            //Fail_TestSet1
            container.RegisterType<ServiceDataRepository>();
            Assert.Throws<TypeInstantiationException>(() => container.CreateInstance<IPersonRepository>());
        }
    }
}