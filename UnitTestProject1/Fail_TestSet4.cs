using Build;

namespace UnitTests
{
    namespace Fail_TestSet4
    {
        public interface IOtherRepository
        {
        }

        public class OtherRepository
        {
        }

        [Dependency("Ho ho ho")]
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository([Injection(typeof(SqlDataRepository))]SqlDataRepository repository)
            {
            }

            [Dependency("UnitTests.Fail_TestSet4.SqlDataRepository")]
            public SqlDataRepository([Injection("UnitTests.Fail_TestSet4.ServiceDataRepository")]ServiceDataRepository repository, int value)
            {
            }

            public Person GetPerson(int personId)
            {
                // get the data from SQL DB and return Person instance.
                return new Person(this);
            }
        }
        
        public class ServiceDataRepository : IPersonRepository
        {
            //public ServiceDataRepository()
            //{
            //}

            [Dependency(typeof(SqlDataRepository))]
            public ServiceDataRepository(SqlDataRepository repository)
            {
            }

            public Person GetPerson(int personId)
            {
                // get the data from Web service and return Person instance.
                return new Person(this);
            }
        }
    }
}
