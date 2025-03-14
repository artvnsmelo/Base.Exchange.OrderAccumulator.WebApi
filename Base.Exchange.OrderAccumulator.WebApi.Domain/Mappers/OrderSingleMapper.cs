using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Mappers;

public static class OrderSingleMapper
{
    public static OrderSingleEntity ToEntity(this ExecutedNewOrderSingleRequest request, OrderSingleEntity symbol, OrderSideEnum side)
    {
        var obj = request.Adapt<OrderSingleEntity>();

        switch (side)
        {
            case OrderSideEnum.BUY:
                obj.FinancialExposure = symbol.FinancialExposure + (request.Price * request.Quantity);
                break;

            case OrderSideEnum.SELL:
                obj.FinancialExposure = symbol.FinancialExposure - (request.Price * request.Quantity);
                break;
        }
        
        obj.UpdateDate = DateTime.Now;
        return obj ?? new();
    }
}
