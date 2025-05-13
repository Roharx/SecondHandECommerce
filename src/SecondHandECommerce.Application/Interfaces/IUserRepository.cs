using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);

}