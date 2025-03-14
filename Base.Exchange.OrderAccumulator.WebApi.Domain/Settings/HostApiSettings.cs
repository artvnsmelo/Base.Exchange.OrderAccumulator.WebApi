namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class HostApiSettings
{
    public const string SectionName = "HostConfig:Api";

    public HostApiCompressionSettings Compression { get; set; } = new();
}
