﻿#pragma warning disable IDE0090

using System;
using Build;

namespace Fail_TestSet2
{
    public interface IPersonRepository
    {
        Person GetPerson(int personId);
    }

    public struct ErrorStruct
    {
        public int PersonId;

        public ErrorStruct()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(ErrorStruct left, ErrorStruct right) => !(left == right);

        public static bool operator ==(ErrorStruct left, ErrorStruct right) => left.Equals(right);

        public override bool Equals(object obj) => throw new NotImplementedException();

        public override int GetHashCode() => throw new NotImplementedException();
    }

    public class ErrorSqlDataRepository : IPersonRepository
    {
        [Dependency("Fail_TestSet2.IPersonRepository")]
        public ErrorSqlDataRepository(ErrorStruct person)
        {
            PersonId = person.PersonId;
        }

        public ErrorSqlDataRepository()
        {
        }

        public int PersonId { get; }

        public Person GetPerson(int personId) => new Person(this);
    }

    public class Person
    {
        readonly IPersonRepository _personRepository;

        public Person(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IPersonRepository Repository { get { return _personRepository; } }
    }

    public class ServiceDataRepository : IPersonRepository
    {
        public ServiceDataRepository([Injection("Fail_TestSet2.IPersonRepository")]int repositoryId) => RepositoryId = repositoryId;

        public IPersonRepository Repository { get; }
        public int RepositoryId { get; }

        public Person GetPerson(int personId) => new(this);
    }

    public class SqlDataRepository : IPersonRepository
    {
        [Dependency("Fail_TestSet2.IPersonRepository")]
        public SqlDataRepository(int personId)
        {
            PersonId = personId;
        }

        public SqlDataRepository()
        {
        }

        public int PersonId { get; }

        public Person GetPerson(int personId) => new(this);
    }
}