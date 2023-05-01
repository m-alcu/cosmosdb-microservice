using CoreApi.Domain.Model;

namespace CoreApi.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();

        Task<Order> GetOrderById(Guid id);

        Task<Guid> CreateOrder(Order order);

        void UpdateOrder(Order order);

        Task<Order> DeleteOrder(Guid id);

    }
}
