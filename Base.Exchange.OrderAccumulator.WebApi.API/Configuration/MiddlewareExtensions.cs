using Base.Exchange.OrderAccumulator.WebApi.API.Middlewares;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;
using Base.Exchange.OrderAccumulator.WebApi.Service.Services;

using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Transport;

namespace Base.Exchange.OrderAccumulator.WebApi.API.Configuration;


[ExcludeFromCodeCoverage]
public static class MiddlewareStartup
{
    public static void UseMiddlewares(this WebApplication app, IConfiguration config)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseStaticFiles();
        app.UseSwaggerDocs();
        app.UseCompression();
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseLocalizationSupport();
        app.UseResponseCaching();
        app.UseMiddleware<CompressionMiddleware>();
        app.UseMiddleware<SecurityHeadersMiddleware>();
        app.UseRouting();        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

    public static void UseCompression(this WebApplication app)
    {
        var hostConfig = app.Services.GetService<IOptions<HostSettings>>()?.Value ?? new();
        var compressionConfig = hostConfig?.Api?.Compression ?? new();

        if (!compressionConfig.Enabled) return;

        app.UseMiddleware<CompressionMiddleware>();
        app.UseResponseCompression();
    }
  
    public static void UseLocalizationSupport(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        var hostConfig = app.ApplicationServices.GetService<IOptions<HostSettings>>()?.Value ?? new();

        var defaultCulture = hostConfig.DefaultCulture;

        app.UseRequestLocalization(options =>
        {
            options.SetDefaultCulture(defaultCulture.Name);
            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        });
    }
}



