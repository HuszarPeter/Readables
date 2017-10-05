using System;
using System.Runtime.Serialization;

namespace Readables.Import.Exceptions
{
    public class UnknownFileTypeException: Exception
    {
        public UnknownFileTypeException()
        {
        }

        public UnknownFileTypeException(string message) : base(message)
        {
        }

        public UnknownFileTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownFileTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
