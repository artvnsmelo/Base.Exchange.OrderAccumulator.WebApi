namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class HostApiCompressionSettings
{
    public const string SectionName = "HostConfig:Api:Compression";

    public bool Enabled { get; set; } = false;
}
