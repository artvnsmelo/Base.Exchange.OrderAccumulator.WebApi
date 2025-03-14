using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Responses;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Services;

public interface IOrderSingleService
{
    Task<bool> ExecuteNewOrderSingleAsync(ExecutedNewOrderSingleRequest request, OrderSideEnum side);
}
