namespace Browser.Requests
{
    using System;

    /// <summary>
    /// The invalid url exception, thrown when an invalid url 
    /// is used in a class that needs a valid one.
    /// </summary>
    public class InvalidUrlException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidUrlException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public InvalidUrlException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}