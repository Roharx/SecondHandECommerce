using MediatR;
using SecondHandECommerce.Application.Users.DTOs;

namespace SecondHandECommerce.Application.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<LoginResponseDto>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}