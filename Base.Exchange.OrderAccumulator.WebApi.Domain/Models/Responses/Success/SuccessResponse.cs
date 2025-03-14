using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Success;

[ExcludeFromCodeCoverage]
public class SuccessResponse<T> : ResponseBase
{
    public T? Data { get; set; } = default!;

    public SuccessResponse(int statusCode, T? data) : base(statusCode)
    {
        Data = data;
    }

    public override ObjectResult ToResult()
    {
        return new ObjectResult(this)
        {
            StatusCode = StatusCode
        };
    }
}
