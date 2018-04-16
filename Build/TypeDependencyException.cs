using System;
using System.Runtime.Serialization;

namespace Build
{
    [Serializable]
    public class TypeDependencyException : Exception
    {
        public TypeDependencyException() { }
        public TypeDependencyException(string message) : base(message) { }
        public TypeDependencyException(string message, Exception innerException) : base(message, innerException) { }
        protected TypeDependencyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }}