namespace OnlineStoreWeb.API.Model;

public class Product
{
    public int Id {get; init;}
    public required string Name {get;set;}
    public required string Description {get;set;}
    public required double Price {get;set;}
    public required string? ImageUrl {get;set;}
    public required int StockQuantity {get;set;}
    public DateTime ProductCreated { get; set; } = DateTime.UtcNow;
    public DateTime ProductUpdated { get; set; }
}