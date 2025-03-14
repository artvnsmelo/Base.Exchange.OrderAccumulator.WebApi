namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

[ExcludeFromCodeCoverage]
public sealed class GenericResponseError : ErrorResponseBase
{
    public GenericResponseError(string errorCode, string errorMessage)
    : base($"exception_{errorCode}", $"Exception Message: {errorMessage}.") { }
}
