namespace SecondHandECommerce.Application.Orders.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public DateTime OrderedAt { get; set; }
    public string Status { get; set; } = default!;
}