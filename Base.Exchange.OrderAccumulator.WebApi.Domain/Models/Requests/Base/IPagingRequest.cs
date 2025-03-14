namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

public interface IPagingRequest
{
    public const int DEFAULT_PAGE = 1;
    public const int DEFAULT_LIMIT = 100;
    public const int DEFAULT_MAX_LIMIT = 1000;

    int Limit { get; set; }
    int Page { get; set; }
}
