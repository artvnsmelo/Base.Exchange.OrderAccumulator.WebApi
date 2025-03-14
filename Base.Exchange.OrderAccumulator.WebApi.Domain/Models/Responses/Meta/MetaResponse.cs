namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses.Meta;

[ExcludeFromCodeCoverage]
public sealed class MetaResponse
{
    public MetaPaginationResponse Pagination { get; set; } = default!;

    public MetaResponse() { }

    public MetaResponse(MetaPaginationResponse pagination)
    {
        Pagination = pagination;
    }
}
