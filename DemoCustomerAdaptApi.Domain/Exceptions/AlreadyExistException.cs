using System;

namespace DemoCustomerAdaptApi.Domain.Exceptions
{
    public class AlreadyExistException : ApiException
    {
        public AlreadyExistException(string message, string errorCode = null)
            : base(message, null, errorCode)
        {
        }

        public AlreadyExistException(string message, string errorCode = null, Exception innerException = null)
            : base(message, null, errorCode, innerException)
        {
        }

        public AlreadyExistException(string name, object key, string errorCode = null)
            : base($"Entity \"{name}\" ({key}) already exist.", null, errorCode)
        {
        }
    }
}