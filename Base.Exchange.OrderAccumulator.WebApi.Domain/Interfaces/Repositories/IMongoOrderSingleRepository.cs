using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;

namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Repositories;

public interface IMongoOrderSingleRepository
{
    Task ExecuteOrderSingleAsync(OrderSingleEntity entityRequest);
    Task<OrderSingleEntity> GetBySymbol(string symbol);
}
