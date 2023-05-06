using CoreApi.Infrastructure;
using CoreApi.Infrastructure.Database;
using CoreApi.Infrastructure.Persistence;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Application.Services;

public class OrderService : IOrderService
{

    private readonly ILogger<OrderService> _logger;
    private readonly AppSettingsService _appSettingsService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationContext _context;

    public OrderService(ILogger<OrderService> logger, AppSettingsService appSettingsService, IUnitOfWork unitOfWork, ApplicationContext context)
    {
        _logger = logger;
        _appSettingsService = appSettingsService;
        _unitOfWork = unitOfWork;
        _context = new ApplicationContext(_appSettingsService);
    }

    public async Task<Guid> CreateOrder(Order order)
    {
        await _context.Database.EnsureCreatedAsync();

        Guid guid = Guid.NewGuid();

        _context.Add(
            new Order(guid, order.TrackingNumber, order.ShippingAddress));

        await _context.SaveChangesAsync();

        _logger.LogInformation("Created Guid {name}!", guid);

        return guid;
    }

    public async Task<Order> DeleteOrder(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order is null)
        {
            return order;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        await _context.Database.EnsureCreatedAsync();

        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> UpdateOrder(Order updatedOrder)
    {

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);

        if (order == null)
        {
            return null;
        }

        order.UpdateOrder(updatedOrder.TrackingNumber, updatedOrder.ShippingAddress);

        _context.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }
}
