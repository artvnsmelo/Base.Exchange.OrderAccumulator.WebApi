using Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Repositories;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;
using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

namespace Base.Exchange.OrderAccumulator.WebApi.Infra.Repositories.Mongo;
public class MongoOrderSingleRepository : MongoRepositoryBase, IMongoOrderSingleRepository
{
    private readonly IMongoCollection<OrderSingleEntity> _collection;

    public MongoOrderSingleRepository(IOptions<MongoDbSettings> dbSettings) : base(dbSettings)
    {
        _collection = GetCollection<OrderSingleEntity>(OrderSingleEntity.CollectionName);
    }

    public async Task ExecuteOrderSingleAsync(OrderSingleEntity entityRequest)
    {
        var filters = Builders<OrderSingleEntity>.Filter.Eq(x => x.Symbol, entityRequest.Symbol);
        var update = Builders<OrderSingleEntity>.Update        
        .Set(x => x.FinancialExposure, entityRequest.FinancialExposure)
        .Set(x => x.UpdateDate, DateTime.Now);

        await _collection.UpdateOneAsync(filters, update);
    }

    public async Task<OrderSingleEntity> GetBySymbol(string symbol)
    {
        var result = await _collection.FindAsync(find => find.Symbol == symbol);
        return await result.FirstOrDefaultAsync();
    }
}
