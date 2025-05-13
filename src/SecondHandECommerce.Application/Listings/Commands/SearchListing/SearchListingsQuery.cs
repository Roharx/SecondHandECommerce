using MediatR;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Listings.Commands.SearchListing;

public record SearchListingsQuery(string? Keyword, decimal? MinPrice, decimal? MaxPrice) : IRequest<List<Listing>>;