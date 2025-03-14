using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Repositories;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Services;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Mappers;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Enums;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Requests;

namespace Base.Exchange.OrderAccumulator.WebApi.Service.Services;

public class OrderSingleService : IOrderSingleService
{
    private readonly IMongoOrderSingleRepository _mongoOrderSingleRepository;

    public OrderSingleService(IMongoOrderSingleRepository mongoOrderSingleRepository)
    {
        _mongoOrderSingleRepository = mongoOrderSingleRepository;
    }

    public async Task<bool> ExecuteNewOrderSingleAsync(ExecutedNewOrderSingleRequest request, OrderSideEnum side)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null");

        var symbol = await _mongoOrderSingleRepository.GetBySymbol(request.Symbol);

        if (symbol == null) throw new KeyNotFoundException("Symbol not found");

        if (!IsFinancialExposureValid(symbol, request, side)) return false;

        await _mongoOrderSingleRepository.ExecuteOrderSingleAsync(request.ToEntity(symbol, side));

        return true;
    }

    public bool IsFinancialExposureValid(OrderSingleEntity symbol, ExecutedNewOrderSingleRequest request, OrderSideEnum side)
    {
        decimal updatedExposure = side == OrderSideEnum.BUY ? symbol.FinancialExposure + request.Quantity * request.Price : symbol.FinancialExposure - request.Quantity * request.Price;

        return updatedExposure <= symbol.FinancialExposureLimit;
    }
}
