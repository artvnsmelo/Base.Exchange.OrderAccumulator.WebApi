using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;

[ExcludeFromCodeCoverage]
public class PagingRequest : BaseRequest, IPagingRequest
{
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_LIMIT = 100;
    public const int DEFAULT_MAX_LIMIT = 1000;

    [FromQuery]
    [Range(1, int.MaxValue)]
    [DefaultValue(DEFAULT_PAGE)]
    public int Page { get; set; } = DEFAULT_PAGE;

    [FromQuery]
    [Range(1, DEFAULT_MAX_LIMIT)]
    [DefaultValue(DEFAULT_LIMIT)]
    public int Limit { get; set; } = DEFAULT_LIMIT;
}
