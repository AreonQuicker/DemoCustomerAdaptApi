namespace DemoCustomerAdaptApi.Domain.Exceptions
{
    public interface IApiException
    {
        string ErrorCode { get; }
        string UserMessage { get; }
    }
}