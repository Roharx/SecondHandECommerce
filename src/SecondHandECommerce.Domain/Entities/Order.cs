using SecondHandECommerce.Domain.Enums;

namespace SecondHandECommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public DateTime OrderedAt { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}