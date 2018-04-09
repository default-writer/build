using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    namespace TestSet2
    {
        //[Dependency(typeof(SqlDataRepository))]
        public class SqlDataRepository : IPersonRepository
        {
            [Dependency("Ho ho ho")]
            public SqlDataRepository()
            {
            }

            //[Dependency("Ho ho ho 2", Runtime.Singleton)]
            public SqlDataRepository(int personId)
            {
            }

            public Person GetPerson(int personId)
            {
                // get the data from SQL DB and return Person instance.
                return new Person(this);
            }
        }

        [Dependency(Runtime.Singleton)]
        public class ServiceDataRepository : IPersonRepository
        {
            public ServiceDataRepository([Inject(typeof(SqlDataRepository))]IPersonRepository repository)
            {
                Repository = repository;
            }
            public IPersonRepository Repository { get; }
            public Person GetPerson(int personId)
            {
                // get the data from Web service and return Person instance.
                return new Person(this);
            }
        }
    }
}
