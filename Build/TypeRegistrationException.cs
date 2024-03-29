using System;
using System.Runtime.Serialization;

namespace Build
{
    /// <summary>
    /// Exception for type registration
    /// </summary>
    /// <seealso cref="Exception"/>
    [Serializable]
    public sealed class TypeRegistrationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeRegistrationException"/> class.
        /// </summary>
        public TypeRegistrationException()
        {
        }

        private TypeRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeRegistrationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TypeRegistrationException(string message) : base(message)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="TypeRegistrationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.
        /// </param>
        public TypeRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}