using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Listings.Commands.CreateListings;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Listings.Commands.CreateListings;

public class CreateListingHandler : IRequestHandler<CreateListingCommand, Guid>
{
    private readonly IListingRepository _repo;

    public CreateListingHandler(IListingRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateListingCommand request, CancellationToken cancellationToken)
    {
        var listing = new Listing
        {
            Id = Guid.NewGuid(),
            SellerId = request.SellerId,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            ImageUrls = request.ImageUrls,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.CreateAsync(listing);
        return listing.Id;
    }
}
