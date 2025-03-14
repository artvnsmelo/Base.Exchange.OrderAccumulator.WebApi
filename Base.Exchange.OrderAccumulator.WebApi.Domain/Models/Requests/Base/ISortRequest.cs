namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;
public class SortItem
{
    public string Sort { get; set; } = string.Empty;
    public string SortDirection { get; set; } = string.Empty;
}

public interface ISortRequest
{
    List<SortItem?> SortItems { get; set; }
}
