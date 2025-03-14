using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Meta;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Success;

[ExcludeFromCodeCoverage]
public class SuccessPagedListResponse<T> : ResponseBase
{
    public MetaResponse Meta { get; set; }
    public IEnumerable<T> Data { get; set; }

    public SuccessPagedListResponse
    (
        int statusCode,
        IPagingRequest paging,
        IEnumerable<T> data,
        int totalItems,
        Uri baseUri
    ) : base(statusCode)
    {
        var cancelPaging = ForceCancelPaging(paging, totalItems, data);
        var total = CalculateTotalItems(data, totalItems);
        var count = data?.Count() ?? 0;

        Meta = GenerateMetaData(paging, count, total, baseUri);
        Data = GenerateTransformedData(Meta, data ?? new List<T>(), cancelPaging);
    }

    private static bool ForceCancelPaging(IPagingRequest paging, int totalItems, IEnumerable<T> data)
    {
        var limit = paging?.Limit ?? IPagingRequest.DEFAULT_LIMIT;
        var page = paging?.Page ?? IPagingRequest.DEFAULT_PAGE;

        var remainingItems = page * limit - totalItems;
        var remainingItemsNonPaging = remainingItems <= 0 || remainingItems < limit;
        var totalItemsGreaterThanZero = totalItems > 0;
        var dataCountBetweenLimit = data?.Count() > 0 && data?.Count() <= limit;

        return

            totalItemsGreaterThanZero &&
            dataCountBetweenLimit &&
            remainingItemsNonPaging
        ;
    }

    private static int CalculateTotalItems(IEnumerable<T> data, int totalItems)
    {
        return totalItems > 0 ? totalItems : data.Count();
    }

    private static MetaResponse GenerateMetaData(IPagingRequest paging, int countItems, int totalItems, Uri baseUri)
    {
        return new MetaResponse(GenerateMetaPagination(paging, countItems, totalItems, baseUri));
    }

    private static MetaPaginationResponse GenerateMetaPagination
    (
        IPagingRequest paging,
        int countItems,
        int totalItems,
        Uri baseUri
    )
    {
        return new MetaPaginationResponse(paging.Page, paging.Limit, countItems, totalItems, baseUri);
    }

    private static IEnumerable<T> GenerateTransformedData
    (
        MetaResponse meta,
        IEnumerable<T> data,
        bool cancelPaging = false
    )
    {
        if (cancelPaging) return data;
        if (meta?.Pagination is null) return data;

        var total = meta?.Pagination?.Total ?? 0;
        var limit = meta?.Pagination?.Limit ?? PagingRequest.DEFAULT_LIMIT;
        var page = meta?.Pagination?.Page ?? PagingRequest.DEFAULT_PAGE;

        return data.Take(total).Skip((page - 1) * limit).Take(limit);
    }

    public override ObjectResult ToResult()
    {
        return new ObjectResult(this)
        {
            StatusCode = StatusCode
        };
    }
}
