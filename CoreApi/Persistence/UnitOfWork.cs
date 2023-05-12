using CoreApi.Application.Services;
using CoreApi.Domain.Exceptions;
using CoreApi.Infrastructure.Database;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreApi.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;
    private readonly ILogger<OrderService> _logger;

    public UnitOfWork(ILogger<OrderService> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
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

        if (order is null)
        {
            throw new DomainException("Order " + updatedOrder.Id + " does not exist");
        }

        order.UpdateOrder(updatedOrder.TrackingNumber, updatedOrder.ShippingAddress);

        _context.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

}
