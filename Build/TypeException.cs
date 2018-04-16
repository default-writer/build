using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeException : Exception
    {
        public TypeException() { }
        public TypeException(string message) : base(message) { }
        public TypeException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}