using System;
using System.Runtime.Serialization;

namespace MSCourse.Web.Exceptions
{
    public class CustomUnAuthorizeException : Exception
    {
        public CustomUnAuthorizeException()
        {
        }

        public CustomUnAuthorizeException(string message) : base(message)
        {
        }

        public CustomUnAuthorizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomUnAuthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
