using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings.GetListingsById;

public class GetListingByIdHandler : IRequestHandler<GetListingByIdQuery, ListingDto?>
{
    private readonly IListingRepository _repo;

    public GetListingByIdHandler(IListingRepository repo)
    {
        _repo = repo;
    }

    public async Task<ListingDto?> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
    {
        var listing = await _repo.GetByIdAsync(request.Id);
        if (listing == null)
            return null;

        return new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Description = listing.Description,
            Price = listing.Price,
            ImageUrls = listing.ImageUrls,
            CreatedAt = listing.CreatedAt
        };
    }
}