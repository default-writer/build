﻿using Build;

namespace UnitTests
{
    namespace Fail_TestSet1
    {
        [DependencyAttribute(typeof(IPersonRepository))]
        public class SqlDataRepository : IPersonRepository
        {
            public SqlDataRepository()
            {
            }

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
            public ServiceDataRepository(IPersonRepository repository)
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