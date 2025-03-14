namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

public class PagingAndSortByRequest : IPagingRequest, ISortRequest
{
    [FromQuery]
    [Range(1, int.MaxValue)]
    [DefaultValue(IPagingRequest.DEFAULT_PAGE)]
    public int Page { get; set; } = IPagingRequest.DEFAULT_PAGE;

    [FromQuery]
    [Range(1, IPagingRequest.DEFAULT_MAX_LIMIT)]
    [DefaultValue(IPagingRequest.DEFAULT_LIMIT)]
    public int Limit { get; set; } = IPagingRequest.DEFAULT_LIMIT;

    [FromQuery]
    [ModelBinder(typeof(SortListEntityBinder))]
    public List<SortItem?> SortItems { get; set; } = new List<SortItem?>();
}
