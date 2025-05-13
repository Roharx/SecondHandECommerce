using MediatR;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings.GetListingsBySeller;

public class GetListingsBySellerQuery : IRequest<List<ListingDto>>
{
    public Guid SellerId { get; set; }

    public GetListingsBySellerQuery(Guid sellerId)
    {
        SellerId = sellerId;
    }
}