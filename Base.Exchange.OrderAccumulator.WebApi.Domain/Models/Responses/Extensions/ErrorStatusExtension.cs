using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Errors;


namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Extensions;

[ExcludeFromCodeCoverage]
public static class ErrorStatusExtension
{
    public static ObjectResult Error(this ControllerBase controller, int statusCode)
        => controller.HttpContext.Error(statusCode);

    public static ObjectResult Error(this ControllerBase controller, int statusCode, string errorCode, string errorMessage)
        => controller.HttpContext.Error(statusCode, errorCode, errorMessage);

    public static ObjectResult Error(this ControllerBase controller, int statusCode, ErrorResponseBase error)
        => controller.HttpContext.Error(statusCode, error);

    public static ObjectResult Error(this HttpContext httpContext, int statusCode)
        => httpContext.CreateErrorListResponse(statusCode).ToResult();

    public static ObjectResult Error(this HttpContext httpContext, int statusCode, string errorCode, string errorMessage)
        => httpContext.CreateErrorListResponse(statusCode).AddError(errorCode, errorMessage).ToResult();

    public static ObjectResult Error(this HttpContext httpContext, int statusCode, ErrorResponseBase error)
        => httpContext.CreateErrorListResponse(statusCode).AddError(error).ToResult();

    private static ErrorListResponse CreateErrorListResponse(this HttpContext httpContext, int statusCode)
        => new(statusCode, httpContext);
}
