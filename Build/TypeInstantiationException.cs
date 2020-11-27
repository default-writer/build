using System;

namespace Build
{
    /// <summary>
    /// Exception for type instantiation
    /// </summary>
    /// <seealso cref="System.Exception"/>
    public sealed class TypeInstantiationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInstantiationException"/> class.
        /// </summary>
        public TypeInstantiationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInstantiationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TypeInstantiationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeInstantiationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        /// Visual Basic) if no inner exception is specified.
        /// </param>
        public TypeInstantiationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}