namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

[ExcludeFromCodeCoverage]
public sealed class DefaultErrorResponse : ErrorResponseBase
{
    public DefaultErrorResponse(string errorCode, string errorMessage) : base(errorCode, errorMessage) { }
}
