using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderRepository(StoreDbContext context, IOrderItemRepository orderItemRepository)
    {
        _context = context;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        try
        {
            return await _context.Orders.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to fetch orders", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task<Order?> GetOrderWithIdAsync(int id)
    {
        try
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to fetch order", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }

    public async Task AddOrderAsync(CreateOrderDto createOrderRequest)
    {
        try
        {
            List<OrderItem> orderItems = await _orderItemRepository.GetAllOrderItemsAsync();
            List<OrderItem> userOrderItems = orderItems.Where(item => item.UserId == createOrderRequest.UserId).ToList();

            Order order = new Order 
            {
                UserId = createOrderRequest.UserId,
                ShippingAddress = createOrderRequest.ShippingAddress,
                PaymentMethod = createOrderRequest.PaymentMethod,
                OrderItems = userOrderItems,
                OrderDate = DateTime.UtcNow,
                Status = Order.OrderStatus.Pending
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to save order", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }


    public async Task DeleteOrderAsync(int id)
    {
        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id)
                ?? throw new Exception("Order not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException("Failed to delete order", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred", ex);
        }
    }
}