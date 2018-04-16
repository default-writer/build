using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeInitializationException : Exception
    {
        public TypeInitializationException() { }
        public TypeInitializationException(string message) : base(message) { }
        public TypeInitializationException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}