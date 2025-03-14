namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

[ExcludeFromCodeCoverage]
public abstract class ResponseBase
{
    public int? StatusCode { get; }
    public string? StatusCodeName => GetStatusCodeName(StatusCode);

    protected ResponseBase(int statusCode)
    {
        StatusCode = statusCode;
    }

    private static string? GetStatusCodeName(int? statusCode)
    {
        return statusCode is null ? null : ReasonPhrases.GetReasonPhrase((int)statusCode) ?? null;
    }

    public abstract ObjectResult ToResult();
}
