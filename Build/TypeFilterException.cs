using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeFilterException : Exception
    {
        public TypeFilterException()
        {
        }

        public TypeFilterException(string message) : base(message)
        {
        }

        public TypeFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}