using Base.Exchange.OrderAccumulator.WebApi.API.Configuration;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

namespace Base.Exchange.OrderAccumulator.WebApi.API.Configuration;


[ExcludeFromCodeCoverage]
public static class CustomHostBuilder
{
    public static WebApplicationBuilder Create(string[] args)
    {
        var builder = CreateBuilder(args);
        var hostConfig = builder.Configuration?.GetSection(HostSettings.SectionName)?.Get<HostSettings>() ?? new();

        Thread.CurrentThread.CurrentCulture = hostConfig.DefaultCulture;
        CultureInfo.DefaultThreadCurrentCulture = hostConfig.DefaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = hostConfig.DefaultCulture;

        builder.Host.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
        });

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);

            options.ConfigureHttpsDefaults(httpsOptions =>
            {
                httpsOptions.AllowAnyClientCertificate();
                httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
            });
        });

        Thread.CurrentThread.CurrentCulture = hostConfig.DefaultCulture;
        CultureInfo.DefaultThreadCurrentCulture = hostConfig.DefaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = hostConfig.DefaultCulture;

        builder.Services.AddOptions();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<HostSettings>(HostSettings.SectionName, builder.Configuration);

        return builder;
    }

    private static WebApplicationBuilder CreateBuilder(params string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddResponseCaching();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<HostSettings>(HostSettings.SectionName, builder.Configuration);

        return builder;
    }

}
