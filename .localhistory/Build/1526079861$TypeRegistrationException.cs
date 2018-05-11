using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeRegistrationException : Exception
    {
        public TypeRegistrationException() { }
        public TypeRegistrationException(string message) : base(message) { }
        public TypeRegistrationException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}