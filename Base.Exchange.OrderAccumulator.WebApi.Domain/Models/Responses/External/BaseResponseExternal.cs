namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.External;
public class BaseResponseExternal<T>
{
    public T? Data { get; set; } = default!;
    public int? StatusCode { get; set; }
    public string? StatusCodeName { get; set; } = string.Empty;
    public MetaResponseExternal? Meta { get; set; } = default!;
    public List<ErrorExternal>? Errors { get; set; } = default!;
}

public class MetaResponseExternal
{
    public PaginationExternal? Pagination { get; set; } = default!;
}

public class PaginationExternal
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
    public Uri? First { get; set; }
    public Uri? Previous { get; set; }
    public Uri? Current { get; set; }
    public Uri? Next { get; set; }
    public Uri? Last { get; set; }
    public bool HasNext { get; set; }
}

public class ErrorExternal
{
    public string? ErrorId { get; set; }
    public string? ErrorTraceId { get; set; }
    public DateTime? ErrorTimestamp { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorTitle { get; set; }
    public string? ErrorDisplay { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorSuggest { get; set; }
    public string? ErrorLanguage { get; set; }
}
