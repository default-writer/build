namespace Build.Tests.Fail_TestSet4
{
    public interface IOtherRepository
    {
    }

    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public class OtherRepository
    {
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
    }

    public class ServiceDataRepository : IPersonRepository
    {
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

    public class ServiceDataRepository2 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository2))]
        public ServiceDataRepository2(object repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository3 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository3))]
        public ServiceDataRepository3(SqlDataRepository3 repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository4 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository4))]
        public ServiceDataRepository4(object repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository5 : IPersonRepository
    {
        //[Dependency(typeof(SqlDataRepository))]
        public ServiceDataRepository5(SqlDataRepository5 repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository6 : IPersonRepository
    {
        [Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository6")]
        public ServiceDataRepository6(object repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class ServiceDataRepository7 : IPersonRepository
    {
        [Dependency(typeof(SqlDataRepository7))]
        public ServiceDataRepository7(object repository)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from Web service and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository : IPersonRepository
    {
        public SqlDataRepository([Injection(typeof(SqlDataRepository))]SqlDataRepository repository)
        {
        }

        [Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository")]
        public SqlDataRepository([Injection("Build.Tests.Fail_TestSet4.ServiceDataRepository")]ServiceDataRepository repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository2 : IPersonRepository
    {
        public SqlDataRepository2([Injection(typeof(SqlDataRepository2))]object repository)
        {
        }

        [Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository2")]
        public SqlDataRepository2([Injection("Build.Tests.Fail_TestSet4.ServiceDataRepository2")]ServiceDataRepository repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository3 : IPersonRepository
    {
        public SqlDataRepository3([Injection(typeof(object))]SqlDataRepository3 repository)
        {
        }

        [Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository3")]
        public SqlDataRepository3([Injection("Build.Tests.Fail_TestSet4.ServiceDataRepository3")]ServiceDataRepository repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository4 : IPersonRepository
    {
        public SqlDataRepository4([Injection(typeof(object))]object repository)
        {
        }

        [Dependency("Build.Tests.Fail_TestSet4.ServiceDataRepository4")]
        public SqlDataRepository4(object repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository5 : IPersonRepository
    {
        public SqlDataRepository5(object repository)
        {
        }

        public SqlDataRepository5([Injection("Build.Tests.Fail_TestSet4.ServiceDataRepository5")]object repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository6 : IPersonRepository
    {
        public SqlDataRepository6([Injection(typeof(object))]object repository)
        {
        }

        //[Dependency("Build.Tests.Fail_TestSet4.SqlDataRepository")]
        public SqlDataRepository6([Injection(typeof(ServiceDataRepository6))]object repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }

    public class SqlDataRepository7 : IPersonRepository
    {
        [Dependency("Build.Tests.Fail_TestSet4.ServiceDataRepository7")]
        public SqlDataRepository7(object repository, int value)
        {
        }

        public Person GetPerson(int personId)
        {
            // get the data from SQL DB and return Person instance.
            return new Person(this);
        }
    }
}