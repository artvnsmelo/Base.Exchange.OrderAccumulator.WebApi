namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class HostSettings
{

    public const string SectionName = "HostConfig";

    public const string DefaultCurrencySymbol = "R$";
    public const string DefaultResourcePath = "Resources";
    public const int DefaultKeepAliveTimeoutInSeconds = 600;
    public const int DefaultRequestHeadersTimeoutInSeconds = 600;
    public static readonly CultureInfo DefaultCultureInfo = new("en-US");
    public static readonly string[] DefaultCulturesNames = new[] { "en-US", "pt-BR" };

    public string CurrencySymbol { get; set; } = DefaultCurrencySymbol;

    public string[] CulturesNames { get; set; } = Array.Empty<string>();

    public string ResourcePath { get; set; } = DefaultResourcePath;

    public CultureInfo DefaultCulture => SupportedCultures?.FirstOrDefault() ?? DefaultCultureInfo;

    public IEnumerable<CultureInfo> SupportedCultures
    {
        get
        {
            var cultures = (CulturesNames?.Length <= 0 ? DefaultCulturesNames : CulturesNames) ?? Array.Empty<string>();
            return cultures.Select(cultureName => new CultureInfo(cultureName)) ?? new List<CultureInfo>();
        }
    }

    public HostApiSettings Api { get; set; } = new();

    public int KeepAliveTimeoutInSeconds { get; set; } = DefaultKeepAliveTimeoutInSeconds;

    public int RequestHeadersTimeoutInSeconds { get; set; } = DefaultRequestHeadersTimeoutInSeconds;

    public TimeSpan KeepAliveTimeout => TimeSpan.FromSeconds
    (
        KeepAliveTimeoutInSeconds > 0 ? KeepAliveTimeoutInSeconds : DefaultKeepAliveTimeoutInSeconds
    );

    public TimeSpan RequestHeadersTimeout => TimeSpan.FromSeconds
    (
        RequestHeadersTimeoutInSeconds > 0 ? RequestHeadersTimeoutInSeconds : DefaultRequestHeadersTimeoutInSeconds
    );
}
