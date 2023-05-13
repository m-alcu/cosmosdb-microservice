using Domain.Entities.Orders;

namespace CoreApi.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task<Guid> CreateOrder(Order order);
    Task<Order> DeleteOrder(Guid id);
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrderById(Guid id);
    Task<Order> UpdateOrder(Order updatedOrder);
    Task SaveChangesAsync();
}
