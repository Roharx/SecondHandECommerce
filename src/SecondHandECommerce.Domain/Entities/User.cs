namespace SecondHandECommerce.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "Buyer";
    public string? ProfileImageUrl { get; set; }
    public double? Rating { get; set; }
}
