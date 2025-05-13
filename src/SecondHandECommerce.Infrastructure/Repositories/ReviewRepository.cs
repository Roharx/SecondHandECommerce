using MongoDB.Driver;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoCollection<Review> _collection;

    public ReviewRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Review>("Reviews");
    }

    public async Task CreateAsync(Review review)
    {
        await _collection.InsertOneAsync(review);
    }

    public async Task<List<Review>> GetBySellerIdAsync(Guid sellerId)
    {
        return await _collection.Find(r => r.SellerId == sellerId).ToListAsync();
    }
}