using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Interfaces;

public interface IReviewRepository
{
    Task CreateAsync(Review review);
    Task<List<Review>> GetBySellerIdAsync(Guid sellerId);
}