using System;
using System.Runtime.Serialization;

namespace Readables.Import.Exceptions
{
    public class ReadableImportException: Exception
    {
        public ReadableImportException()
        {
        }

        public ReadableImportException(string message) : base(message)
        {
        }

        public ReadableImportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReadableImportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
