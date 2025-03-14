// using Genial.B2b.Partner.Customer.Core.WebApi.Domain.Enums;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests.Base;

public class BaseRequest
{
    [FromRoute]
    [BindRequired]
    [Range(1, long.MaxValue)]
    public long AccountNumber { get; set; }

    // [FromQuery]
    // [BindRequired]
    // public FilterTypeEnum FilterType { get; set; }

    [FromQuery]
    public DateTime? StartDate { get; set; }

    [FromQuery]
    public DateTime? EndDate { get; set; }

    [FromQuery]
    [Range(0, long.MaxValue)]
    public long? AdvisorId { get; set; }

    [FromQuery]
    [Range(0, long.MaxValue)]
    public long? PartnerId { get; set; }

    public bool AdvisorIdIsValid() => AdvisorId is > 0;

    public bool PartnerIdIsValid() => PartnerId is > 0;
}
