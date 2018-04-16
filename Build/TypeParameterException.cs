using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeInjectionException : Exception
    {
        public TypeInjectionException() { }
        public TypeInjectionException(string message) : base(message) { }
        public TypeInjectionException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeInjectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}