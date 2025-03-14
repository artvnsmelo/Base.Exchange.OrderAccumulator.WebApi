namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

public class SortRequest : ISortRequest
{
    [FromQuery]
    [ModelBinder(typeof(SortListEntityBinder))]
    public List<SortItem?> SortItems { get; set; } = new List<SortItem?>();
}
