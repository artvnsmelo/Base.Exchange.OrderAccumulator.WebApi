namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Interfaces.Repositories;

public interface IRedisRepository
{
    Task<T?> GetAsync<T>(string key);
    Task<T?> SetAsync<T>(string key, T? value, TimeSpan? ttlExpiry);
}
