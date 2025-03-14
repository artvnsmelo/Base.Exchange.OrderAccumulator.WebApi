using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Success;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Extensions;

[ExcludeFromCodeCoverage]
public static class StatusHelperExtension
{
    public static ActionResult Success(this ControllerBase _, int statusCode)
    {
        return new StatusCodeResult(statusCode);
    }

    public static ActionResult<SuccessResponse<T>> Success<T>(this ControllerBase _, int statusCode, T? data)
        where T : class
    {
        return new SuccessResponse<T>(statusCode, data).ToResult();
    }

    public static ActionResult<SuccessListResponse<T>> Success<T>(this ControllerBase _, int statusCode, IEnumerable<T>? data = null)
        where T : class
    {
        return new SuccessListResponse<T>(statusCode, data ?? new List<T>()).ToResult();
    }

    public static ActionResult<SuccessPagedListResponse<T>> Success<T>
    (
        this ControllerBase controller,
        int statusCode,
        IPagingRequest paging,
        IEnumerable<T>? data = null,
        int? totalItems = 0
    ) where T : class
    {
        return new SuccessPagedListResponse<T>
        (
            statusCode,
            paging,
            data ?? new List<T>(),
            totalItems ?? 0,
            GetBaseUri(controller.Request.Path, controller.Request.Query)
        ).ToResult();
    }

    private static Uri GetBaseUri(PathString path, IQueryCollection? query = null)
    {
        var queryStringItems = query?.ToDictionary(c => c.Key, c => c.Value) ?? new();
        queryStringItems.Remove("page");
        queryStringItems.Remove("limit");

        var queryString = QueryString.Create(queryStringItems.ToList());
        var baseUri = UriHelper.BuildRelative(path, query: queryString);

        return new Uri(baseUri, UriKind.Relative);
    }
}
