public record UpdateProductDto
{
    public string Name {get;set;}
    public string Description {get;set;}
    public double Price {get;set;}
    public string? ImageUrl {get;set;}
    public int StockQuantity {get;set;}
}