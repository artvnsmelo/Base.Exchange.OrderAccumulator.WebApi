namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Results;

public sealed class DataResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; } = default;
    public TotalItemsResult TotalItems { get; set; } = 0;

    public DataResult() { }

    public DataResult(bool success, string? message = null)
        : this(default, success, message ?? string.Empty) { }

    public DataResult(T? data)
        : this(data, true, string.Empty) { }

    public DataResult(T? data, bool success, string message)
        : this(data, 0, success, message) { }

    public DataResult(T? data, int totalItems)
        : this(data, totalItems, true, string.Empty) { }

    public DataResult(T? data, int totalItems, bool success, string message)
    {
        Success = success;
        Message = message;
        Data = data;
        TotalItems = totalItems;
    }
}
