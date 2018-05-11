using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeInstantiationException : Exception
    {
        public TypeInstantiationException()
        {
        }

        public TypeInstantiationException(string message) : base(message)
        {
        }

        public TypeInstantiationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeInstantiationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}