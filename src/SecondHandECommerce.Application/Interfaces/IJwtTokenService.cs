using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}