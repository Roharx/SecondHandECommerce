using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings.GetAllListings;

public class GetAllListingsHandler : IRequestHandler<GetAllListingsQuery, List<ListingDto>>
{
    private readonly IListingRepository _repo;

    public GetAllListingsHandler(IListingRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ListingDto>> Handle(GetAllListingsQuery request, CancellationToken cancellationToken)
    {
        var listings = await _repo.GetAllAsync();

        return listings.Select(listing => new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Description = listing.Description,
            Price = listing.Price,
            ImageUrls = listing.ImageUrls,
            CreatedAt = listing.CreatedAt
        }).ToList();
    }
}