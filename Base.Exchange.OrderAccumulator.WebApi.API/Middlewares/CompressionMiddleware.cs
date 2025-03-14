namespace Base.Exchange.OrderAccumulator.WebApi.API.Middlewares;
[ExcludeFromCodeCoverage]
public class CompressionMiddleware
{
    private readonly RequestDelegate _next = default!;

    public CompressionMiddleware() { }

    public CompressionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var acceptEncoding = httpContext.Request.Headers["Accept-Encoding"].ToString() ?? string.Empty;

        if (!acceptEncoding.ToUpper().Contains("GZIP", StringComparison.OrdinalIgnoreCase))
        {
            httpContext.Request.Headers["Accept-Encoding"] += ",gzip";
        }

        await _next(httpContext);
    }
}
