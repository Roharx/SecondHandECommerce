using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings.GetListingsBySeller;

public class GetListingsBySellerHandler : IRequestHandler<GetListingsBySellerQuery, List<ListingDto>>
{
    private readonly IListingRepository _listingRepo;
    private readonly IUserRepository _userRepo;

    public GetListingsBySellerHandler(IListingRepository listingRepo, IUserRepository userRepo)
    {
        _listingRepo = listingRepo;
        _userRepo = userRepo;
    }

    public async Task<List<ListingDto>> Handle(GetListingsBySellerQuery request, CancellationToken cancellationToken)
    {
        var seller = await _userRepo.GetByIdAsync(request.SellerId);
        if (seller == null)
            return new List<ListingDto>();

        var listings = await _listingRepo.GetBySellerIdAsync(request.SellerId);

        return listings.Select(listing => new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Description = listing.Description,
            Price = listing.Price,
            ImageUrls = listing.ImageUrls,
            CreatedAt = listing.CreatedAt,
            SellerName = seller.Name,
            SellerEmail = seller.Email
        }).ToList();
    }
}