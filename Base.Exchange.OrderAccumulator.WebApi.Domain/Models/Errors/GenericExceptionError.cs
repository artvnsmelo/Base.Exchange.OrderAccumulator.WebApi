using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Errors
{
    [ExcludeFromCodeCoverage]
    public class GenericExceptionError : ErrorResponseBase
    {
        public GenericExceptionError() : base("exception_error") { }
        public GenericExceptionError(string errorCode, string errorMessage)
        : base($"exception_{errorCode}", errorMessage) { }
    }
}
