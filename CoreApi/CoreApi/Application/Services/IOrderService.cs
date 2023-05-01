using CoreApi.Domain.Model;

namespace CoreApi.Application.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();

        Order GetOrderById(Guid id);

        void CreateOrder(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Guid id);

    }
}
