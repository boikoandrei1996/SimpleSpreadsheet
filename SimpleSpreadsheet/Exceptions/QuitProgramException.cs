using System;
using System.Runtime.Serialization;

namespace SimpleSpreadsheet.Exceptions
{
    public class QuitProgramException : Exception
    {
        public QuitProgramException()
        {
        }

        public QuitProgramException(string message) : base(message)
        {
        }

        public QuitProgramException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QuitProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
