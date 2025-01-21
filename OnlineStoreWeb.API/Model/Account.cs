namespace OnlineStoreWeb.API.Model;

public class Account
{
    public int Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public DateTime UserCreated = DateTime.UtcNow;
    public DateTime? UserUpdated { get; set; }
}