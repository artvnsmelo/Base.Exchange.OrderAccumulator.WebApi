namespace Base.Exchange.OrderAccumulator.WebApi.Domain.Models.Entities;

public class EntityBase
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString() ?? string.Empty;

    [BsonElement("createDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
    public DateTime? CreateDate { get; set; }

    [BsonElement("updateDate")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
    public DateTime? UpdateDate { get; set; }
}
