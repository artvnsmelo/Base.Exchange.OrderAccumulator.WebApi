using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;

[ExcludeFromCodeCoverage]
public sealed class ErrorListResponse : ResponseBase
{
    private readonly List<ErrorResponseBase> _errors;

    private readonly HttpContext _httpContext;

    public IEnumerable<ErrorResponseBase> Errors => _errors;

    public ErrorListResponse(int statusCode, HttpContext httpContext) : base(statusCode)
    {
        _httpContext = httpContext;
        _errors = new List<ErrorResponseBase>();
    }

    public ErrorListResponse AddError(string errorCode, string errorMessage)
        => AddError(new DefaultErrorResponse(errorCode, errorMessage));

    public ErrorListResponse AddError(ErrorResponseBase error)
    {
        error.AutoGenerateFields(_httpContext);

        _errors.Add(error);

        return this;
    }

    public override ObjectResult ToResult()
    {
        return new ObjectResult(this)
        {
            StatusCode = StatusCode
        };
    }
}
