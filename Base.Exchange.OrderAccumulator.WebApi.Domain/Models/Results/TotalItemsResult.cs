namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Results;

public class TotalItemsResult
{
    public int Count { get; set; } = 0;

    public TotalItemsResult() { }

    public TotalItemsResult(int count)
    {
        Count = count;
    }

    public static implicit operator TotalItemsResult(int count)
    {
        return new(count);
    }
}
