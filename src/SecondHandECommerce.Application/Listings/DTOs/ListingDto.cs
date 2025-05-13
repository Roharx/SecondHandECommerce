namespace SecondHandECommerce.Application.Listings.DTOs;

public class ListingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public List<string> ImageUrls { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public string? SellerName { get; set; }
    public string? SellerEmail { get; set; }
}