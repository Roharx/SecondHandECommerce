using MediatR;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings;

public class GetListingByIdQuery : IRequest<ListingDto>
{
    public Guid Id { get; set; }

    public GetListingByIdQuery(Guid id)
    {
        Id = id;
    }
}