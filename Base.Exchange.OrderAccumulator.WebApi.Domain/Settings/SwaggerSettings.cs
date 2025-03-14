namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

[ExcludeFromCodeCoverage]
public class SwaggerSettings
{
    public const string SectionName = "SwaggerConfig";

    public string Title { get; set; } = "API";
    public string Version { get; set; } = "v1";
    public string Description { get; set; } = string.Empty;
    public string TermsOfService { get; set; } = string.Empty;
    public Uri? TermsOfServiceUri => string.IsNullOrWhiteSpace(TermsOfService) ? null : new(TermsOfService);
    public string ContactName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactUrl { get; set; } = string.Empty;
    public Uri? ContactUri => string.IsNullOrWhiteSpace(ContactUrl) ? null : new(ContactUrl);
    public string LicenseName { get; set; } = string.Empty;
    public string LicenseUrl { get; set; } = string.Empty;
    public Uri? LicenseUri => string.IsNullOrWhiteSpace(LicenseUrl) ? null : new(LicenseUrl);
    public string EndpointUrl { get; set; } = string.Empty;
    public Uri? EndpointUri => string.IsNullOrWhiteSpace(EndpointUrl) ? null : new(EndpointUrl);
    public string EndpointName { get; set; } = string.Empty;
    public SwaggerSecuritySettings Security { get; set; } = new();
    public SwaggerServerSettings Server { get; set; } = new();
}
