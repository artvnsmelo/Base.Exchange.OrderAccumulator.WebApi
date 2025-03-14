namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class SwaggerSecuritySettings
{
    public const string SectionName = "SwaggerConfig:Security";

    public bool Enabled { get; set; } = false;

    public string Scheme { get; set; } = string.Empty;
}
