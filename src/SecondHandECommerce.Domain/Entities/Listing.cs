using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SecondHandECommerce.Domain.Entities;

public class Listing
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public List<string> ImageUrls { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}