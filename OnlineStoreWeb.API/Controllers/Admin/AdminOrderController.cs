using Microsoft.AspNetCore.Mvc;
using OnlineStoreWeb.API.DTO.Request.Order;
using OnlineStoreWeb.API.Services.Order;

namespace OnlineStoreWeb.API.Controllers.Admin;

[ApiController]
[Route("api/admin/orders")]
public class AdminOrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public AdminOrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(new { message = "Orders fetched successfully", data = orders });
        }
        catch (Exception exception)
        {
            return BadRequest( exception.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _orderService.GetOrderWithIdAsync(id);
            return Ok(new { message = "Order fetched successfully", data = order });
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            await _orderService.DeleteOrderByAccountIdAsync(id);
            return Ok(new { message = "Order deleted successfully with id: " + id });
        }
        catch (Exception exception) 
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, OrderUpdateDto orderUpdateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _orderService.UpdateOrderStatusByAccountIdAsync(id, orderUpdateDto);
            return Ok(new { message = "Order status updated successfully with id: " + id });
        }
        catch (Exception exception)
        {
            return BadRequest( exception.Message);
        }
    }
}