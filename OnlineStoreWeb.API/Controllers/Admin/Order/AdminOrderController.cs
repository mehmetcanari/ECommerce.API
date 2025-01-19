using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreWeb.API.Controllers.Admin.Order;

[ApiController]
[Route("api/admin/orders")]
public class AdminOrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<AdminOrderController> _logger;

    public AdminOrderController(IOrderRepository orderRepository, ILogger<AdminOrderController> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(new { message = "Orders fetched successfully", data = orders });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching orders: {Message}", ex.Message);
            return StatusCode(500, "An unexpected error occurred while fetching orders");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrderWithIdAsync(id);
            return Ok(new { message = "Order fetched successfully", data = order });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching order: {Message}", ex.Message);
            return StatusCode(500, "An unexpected error occurred while fetching the order");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            await _orderRepository.DeleteOrderAsync(id);
            return Ok(new { message = "Order deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while deleting order: {Message}", ex.Message);
            return StatusCode(500, "An unexpected error occurred while deleting the order");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus orderStatus)
    {
        try
        {
            await _orderRepository.UpdateOrderStatusAsync(id, orderStatus);
            return Ok(new { message = "Order status updated successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while updating order status: {Message}", ex.Message);
            return StatusCode(500, "An unexpected error occurred while updating the order status");
        }
    }
}