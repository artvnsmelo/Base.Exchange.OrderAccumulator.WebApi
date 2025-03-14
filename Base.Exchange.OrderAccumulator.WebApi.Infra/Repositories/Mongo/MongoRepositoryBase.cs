using Base.Exchange.OrderAccumulator.WebApi.Domain.Settings;

namespace Base.Exchange.OrderAccumulator.WebApi.Infra.Repositories.Mongo;

[ExcludeFromCodeCoverage]
public abstract class MongoRepositoryBase
{
    protected IMongoClient Client { get; }
    private IMongoDatabase Database { get; }
    protected MongoDbSettings Settings { get; }

    public MongoRepositoryBase(IOptions<MongoDbSettings> settings)
    {
        Settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

        Client = new MongoClient(Settings.ConnectionString);
        Database = Client.GetDatabase(Settings.DatabaseName);
    }

    protected IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName)
    {
        return Database.GetCollection<TCollection>(collectionName);
    }

    protected IMongoClient GetClient()
    {
        return Client;
    }
}
