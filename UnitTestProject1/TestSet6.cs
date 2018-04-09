using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    namespace TestSet6
    {
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository()
            {
            }

            [Dependency("Ho ho ho")]
            public SqlDataRepository(int personId)
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
            public ServiceDataRepository([Inject("Ho ho ho")]IPersonRepository repository)
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
