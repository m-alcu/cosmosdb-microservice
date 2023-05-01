using CoreApi.Domain.Model;

namespace CoreApi.Application.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(Guid id);

        Task<Guid> CreateOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Guid id);

    }
}
