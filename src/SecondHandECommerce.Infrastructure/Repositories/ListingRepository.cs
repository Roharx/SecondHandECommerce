using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Infrastructure.Repositories;

public class ListingRepository : IListingRepository
{
    private readonly IMongoCollection<Listing> _collection;
    private readonly IMemoryCache _cache;
    private const string AllListingsCacheKey = "all_listings";

    public ListingRepository(IMongoDatabase database, IMemoryCache cache)
    {
        _collection = database.GetCollection<Listing>("Listings");
        _cache = cache;
    }

    public async Task CreateAsync(Listing listing)
    {
        await _collection.InsertOneAsync(listing);
        _cache.Remove(AllListingsCacheKey);
    }

    public async Task<Listing?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(l => l.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Listing>> GetAllAsync()
    {
        if (_cache.TryGetValue(AllListingsCacheKey, out List<Listing> cached))
            return cached;

        var listings = await _collection.Find(_ => true).ToListAsync();
        _cache.Set(AllListingsCacheKey, listings, TimeSpan.FromMinutes(5));
        return listings;
    }

    public async Task<List<Listing>> GetBySellerIdAsync(Guid sellerId)
    {
        return await _collection.Find(l => l.SellerId == sellerId).ToListAsync();
    }
    
    public async Task<List<Listing>> SearchAsync(string? keyword, decimal? minPrice, decimal? maxPrice)
    {
        var filters = new List<FilterDefinition<Listing>>();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var keywordFilter = Builders<Listing>.Filter.Or(
                Builders<Listing>.Filter.Regex(x => x.Title, new BsonRegularExpression(keyword, "i")),
                Builders<Listing>.Filter.Regex(x => x.Description, new BsonRegularExpression(keyword, "i"))
            );
            filters.Add(keywordFilter);
        }

        if (minPrice.HasValue)
            filters.Add(Builders<Listing>.Filter.Gte(x => x.Price, minPrice.Value));

        if (maxPrice.HasValue)
            filters.Add(Builders<Listing>.Filter.Lte(x => x.Price, maxPrice.Value));

        var finalFilter = filters.Count > 0
            ? Builders<Listing>.Filter.And(filters)
            : Builders<Listing>.Filter.Empty;

        return await _collection.Find(finalFilter).ToListAsync();
    }
}