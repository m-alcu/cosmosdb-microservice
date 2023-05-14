using Domain.Entities.Orders;

namespace CoreApi.Application.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders(CancellationToken cancellationToken);

    Task<Order> GetOrderById(Guid id);

    Task<Guid> CreateOrder(Order order);

    Task<Order> UpdateOrder(Order order);

    Task<Order> DeleteOrder(Guid id);

}
