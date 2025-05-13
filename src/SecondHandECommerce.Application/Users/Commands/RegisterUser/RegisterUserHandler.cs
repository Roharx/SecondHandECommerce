using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Users.DTOs;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Users.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _repo;

    public RegisterUserHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Name = request.Name,
            PasswordHash = passwordHash,
            Role = "Buyer"
        };


        await _repo.CreateAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            ProfileImageUrl = user.ProfileImageUrl,
            Rating = (float?)user.Rating
        };
    }
}