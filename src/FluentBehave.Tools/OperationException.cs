using System;

namespace FluentBehave.Tools
{
    public class OperationException : Exception
    {
        public OperationException(string message)
            : base(message)
        {
        }

        public OperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
