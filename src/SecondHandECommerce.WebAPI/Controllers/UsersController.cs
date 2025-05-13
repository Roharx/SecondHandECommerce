using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandECommerce.Application.Users.Commands.RegisterUser;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Users.Commands.LoginUser;
using SecondHandECommerce.Application.Users.DTOs;

namespace SecondHandECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepo;

    public UsersController(IMediator mediator, IUserRepository userRepo)
    {
        _mediator = mediator;
        _userRepo = userRepo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name,
            ProfileImageUrl = user.ProfileImageUrl,
            Rating = (float?)user.Rating
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("register-seller")]
    public async Task<IActionResult> RegisterSeller(RegisterUserCommand command)
    {
        command.Role = "Seller";
        var result = await _mediator.Send(command);
        return Ok(result);
    }

}