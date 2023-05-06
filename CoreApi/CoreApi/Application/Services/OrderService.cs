using CoreApi.Infrastructure;
using CoreApi.Infrastructure.Database;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Application.Services;

public class OrderService : IOrderService
{

    private readonly ILogger<OrderService> _logger;
    private readonly AppSettingsService _appSettingsService;

    public OrderService(ILogger<OrderService> logger, AppSettingsService appSettingsService)
    {
        _logger = logger;
        _appSettingsService = appSettingsService;
    }

    public async Task<Guid> CreateOrder(Order order)
    {
        using (var context = new OrderContext(_appSettingsService))
        {
            await context.Database.EnsureCreatedAsync();

            Guid guid = Guid.NewGuid();

            context.Add(
                new Order(guid, order.TrackingNumber, order.ShippingAddress));

            await context.SaveChangesAsync();

            _logger.LogInformation("Created Guid {name}!", guid);

            return guid;
        }
    }

    public async Task<Order> DeleteOrder(Guid id)
    {
        using (var context = new OrderContext(_appSettingsService))
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
            {
                return order;
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return order;
        }
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        using (var context = new OrderContext(_appSettingsService))
        {
            await context.Database.EnsureCreatedAsync();

            return await context.Orders.ToListAsync();

        }
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        using (var context = new OrderContext(_appSettingsService))
        {
            return await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
    }

    public async Task<Order> UpdateOrder(Order updatedOrder)
    {
        using (var context = new OrderContext(_appSettingsService))
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);

            if (order == null)
            {
                return null;
            }

            order.UpdateOrder(updatedOrder.TrackingNumber, updatedOrder.ShippingAddress);

            context.Update(order);
            await context.SaveChangesAsync();

            return order;
        }
    }
}
