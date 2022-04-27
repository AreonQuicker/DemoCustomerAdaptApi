using System;

namespace DemoCustomerAdaptApi.Domain.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message, string errorCode = null)
            : base(message, null, errorCode)
        {
        }

        public NotFoundException(string message, string errorCode = null, Exception innerException = null)
            : base(message, null, errorCode, innerException)
        {
        }

        public NotFoundException(string name, object key, string errorCode = null)
            : base($"Entity \"{name}\" ({key}) was not found.", null, errorCode)
        {
        }
    }
}