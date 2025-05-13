using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Interfaces;

public interface IListingRepository
{
    Task CreateAsync(Listing listing);
    Task<Listing?> GetByIdAsync(Guid id);
    Task<List<Listing>> GetAllAsync();
    Task<List<Listing>> GetBySellerIdAsync(Guid sellerId);
    Task<List<Listing>> SearchAsync(string? keyword, decimal? minPrice, decimal? maxPrice);

}