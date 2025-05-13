using MediatR;
using SecondHandECommerce.Application.Listings.DTOs;

namespace SecondHandECommerce.Application.Listings.Queries.GetListings.GetAllListings;

public class GetAllListingsQuery : IRequest<List<ListingDto>> { }