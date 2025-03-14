using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

public class ExecutionReportResponse
{
    public ExecutionTypeEnum ExecType { get; set; }
}
