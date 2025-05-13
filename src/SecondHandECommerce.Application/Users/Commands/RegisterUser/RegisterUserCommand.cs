using MediatR;
using SecondHandECommerce.Application.Listings.DTOs;
using SecondHandECommerce.Application.Users.DTOs;

namespace SecondHandECommerce.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<UserDto>
{
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = "Buyer";
}
