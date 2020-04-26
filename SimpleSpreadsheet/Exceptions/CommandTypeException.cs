using System;
using System.Runtime.Serialization;

namespace SimpleSpreadsheet.Exceptions
{
    public class CommandTypeException : Exception
    {
        public CommandTypeException()
        {
        }

        public CommandTypeException(string message) : base(message)
        {
        }

        public CommandTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
