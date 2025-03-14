namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Meta;

[ExcludeFromCodeCoverage]
public sealed class MetaPaginationResponse
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
    public Uri First { get; set; } = default!;
    public Uri Previous { get; set; } = default!;
    public Uri Current { get; set; } = default!;
    public Uri Next { get; set; } = default!;
    public Uri Last { get; set; } = default!;
    public bool HasNext { get; set; }

    public MetaPaginationResponse() { }

    public MetaPaginationResponse(int page, int limit, int count, int total, Uri baseUri)
    {
        Page = page;
        Limit = limit;
        Count = count;
        Total = total;
        First = GenerateFirstUri(baseUri, Limit);
        Previous = GeneratePreviousUri(baseUri, Page, Limit, Total);
        Current = GenerateUri(baseUri, Page, Limit);
        Next = GenerateNextUri(baseUri, Page, Limit, Total);
        Last = GenerateLastUri(baseUri, Limit, Total);
        HasNext = CalculateHasNext(Page, Limit, Total);
    }

    private static Uri GenerateFirstUri(Uri baseUri, int limit)
    {
        return GenerateUri(baseUri, 1, limit);
    }

    private static Uri GeneratePreviousUri(Uri baseUri, int page, int limit, int total)
    {
        var previousPage = page > 1 ? page - 1 : page;

        return previousPage * limit >= total
            ? GenerateLastUri(baseUri, limit, total)
            : GenerateUri(baseUri, previousPage, limit);
    }

    private static Uri GenerateNextUri(Uri baseUri, int page, int limit, int total)
    {
        var remaining = total - (page + 1) * limit;

        return remaining <= 0
            ? GenerateLastUri(baseUri, limit, total)
            : GenerateUri(baseUri, page + 1, limit);
    }

    private static Uri GenerateLastUri(Uri baseUri, int limit, int total)
    {
        int lastPage = GetLastPage(limit, total);

        return GenerateUri(baseUri, lastPage, limit);
    }

    private static Uri GenerateUri(Uri baseUri, int page, int limit)
    {
        var uriSeparator = GetUriSeparator(baseUri);
        return new Uri($"{baseUri}{uriSeparator}page={page}&limit={limit}", UriKind.Relative);
    }

    private static string GetUriSeparator(Uri baseUri)
    {
        return baseUri.ToString().Contains('?') ? "&" : "?";
    }

    private static bool CalculateHasNext(int page, int limit, int total)
    {
        var lastPage = GetLastPage(limit, total);
        return page >= 1 && page < lastPage;
    }

    private static int GetLastPage(int limit, int total)
    {
        var pageCalculation = (int)Math.Round((decimal)total / limit, MidpointRounding.ToPositiveInfinity);
        return pageCalculation < 1 ? 1 : pageCalculation;
    }
}
