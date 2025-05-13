using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Interfaces;

public interface IOrderRepository
{
    Task CreateAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>> GetByBuyerIdAsync(Guid buyerId);
    Task UpdateAsync(Order order);

}