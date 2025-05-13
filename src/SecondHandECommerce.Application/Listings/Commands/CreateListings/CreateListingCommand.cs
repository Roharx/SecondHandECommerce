using MediatR;

namespace SecondHandECommerce.Application.Listings.Commands.CreateListings;

public class CreateListingCommand : IRequest<Guid>
{
    public Guid SellerId { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public List<string> ImageUrls { get; set; } = new();
}