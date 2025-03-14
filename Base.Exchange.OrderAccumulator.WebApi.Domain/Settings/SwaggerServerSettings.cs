namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class SwaggerServerSettings
{
    public const string SectionName = "SwaggerConfig:Server";

    public string Url { get; set; } = string.Empty;

    public Uri? Uri => string.IsNullOrWhiteSpace(Url) ? null : new(Url);
}
