using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Success;

[ExcludeFromCodeCoverage]
public class SuccessListResponse<T> : ResponseBase
{
    public IEnumerable<T> Data { get; private set; }

    public SuccessListResponse(int statusCode, IEnumerable<T> data) : base(statusCode)
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
