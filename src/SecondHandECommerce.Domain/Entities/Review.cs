using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecondHandECommerce.Domain.Entities;

public class Review
{
    public Guid Id { get; set; }
    
    public Guid BuyerId { get; set; }
    public Guid SellerId { get; set; }

    public string Comment { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}