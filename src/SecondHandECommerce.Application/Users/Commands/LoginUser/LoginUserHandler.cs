using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Users.DTOs;

namespace SecondHandECommerce.Application.Users.Commands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepo;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUserHandler(IUserRepository userRepo, IJwtTokenService jwtTokenService)
    {
        _userRepo = userRepo;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = _jwtTokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            Role = user.Role,
            Name = user.Name
        };
    }
}