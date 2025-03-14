using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Repositories;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Services;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;
using Base.Exchange.OrderAccumulator.WebApi.Infra.Repositories.Mongo;
using Base.Exchange.OrderAccumulator.WebApi.Service.Services;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix;

namespace Base.Exchange.OrderAccumulator.WebApi.API.Configuration;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfiguration config)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddOptions(config);
        services.AddServices();        
        services.AddRepositories();
        services.AddCompression();
        services.AddSwaggerDocs();
        services.AddControllers();
        services.AddJsonSerializer();
        services.AddLocalizationSupport();
        services.AddQuickFix();
    }

    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {                
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));
    }
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderSingleService, OrderSingleService>();        
    }

    public static void AddQuickFix(this IServiceCollection services)
    {        
        var orderSingleService = services.BuildServiceProvider().GetRequiredService<IOrderSingleService>();
        var fixApp = new FixAcceptor(orderSingleService);

        var fixConfigOptions = services.BuildServiceProvider().GetService<IOptions<QuickFixSettings>>();
        var fixCfg = fixConfigOptions?.Value ?? new();

        var settings = new SessionSettings(fixCfg.Config);
        var storeFactory = new FileStoreFactory(settings);
        var logFactory = new FileLogFactory(settings);
        
        var acceptor = new ThreadedSocketAcceptor(fixApp, storeFactory, settings, logFactory);   
        
        services.AddSingleton(acceptor);
        acceptor.Start();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMongoOrderSingleRepository, MongoOrderSingleRepository>();
    }

    public static void AddCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
            options.EnableForHttps = true;
        });

        services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
        services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
    }

    public static void AddJsonSerializer(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }

    public static void AddLocalizationSupport(this IServiceCollection services)
    {
        var hostConfigOptions = services.BuildServiceProvider().GetService<IOptions<HostSettings>>();
        var hostConfig = hostConfigOptions?.Value ?? new();

        var defaultCultureName = hostConfig.DefaultCulture.Name;
        var supportedCultures = hostConfig.SupportedCultures?.ToList() ?? new();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture(defaultCultureName);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture(defaultCultureName);
        });
    }
}
