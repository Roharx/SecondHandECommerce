using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Listings.Commands.SearchListing;

public class SearchListingsHandler : IRequestHandler<SearchListingsQuery, List<Listing>>
{
    private readonly IListingRepository _repository;
    private readonly IMemoryCache _cache;

    public SearchListingsHandler(IListingRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<Listing>> Handle(SearchListingsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"search:{request.Keyword}:{request.MinPrice}:{request.MaxPrice}";

        if (_cache.TryGetValue(cacheKey, out List<Listing> cachedListings))
        {
            return cachedListings;
        }

        var results = await _repository.SearchAsync(request.Keyword, request.MinPrice, request.MaxPrice);

        _cache.Set(cacheKey, results, TimeSpan.FromMinutes(5));

        return results;
    }
}