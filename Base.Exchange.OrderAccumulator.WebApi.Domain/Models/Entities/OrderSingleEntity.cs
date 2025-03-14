namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("OrderSingle")]
public class OrderSingleEntity : EntityBase
{
    public const string CollectionName = "OrderSingle";

    [BsonElement("Symbol")]
    public string Symbol { get; set; } = string.Empty;

    [BsonElement("FinancialExposure")]
    public decimal FinancialExposure { get; set; }

    [BsonElement("FinancialExposureLimit")]
    public decimal FinancialExposureLimit { get; set; }
}
