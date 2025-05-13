namespace SecondHandECommerce.Application.Users.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ProfileImageUrl { get; set; }
    public float? Rating { get; set; }
}