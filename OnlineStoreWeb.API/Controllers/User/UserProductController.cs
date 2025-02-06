using Microsoft.AspNetCore.Mvc;
using OnlineStoreWeb.API.Services.Product;

namespace OnlineStoreWeb.API.Controllers.User;

[ApiController]
[Route("api/user/products")]
public class UserProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(new { message = "Products fetched successfully", data = products });
        }
        catch 
        {
            return StatusCode(500, "An unexpected error occurred while fetching products");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await productService.GetProductWithIdAsync(id);
            return Ok(new { message = "Product fetched successfully", data = product });
        }
        catch 
        {
            return StatusCode(500, "An unexpected error occurred while fetching the product");
        }
    }
}