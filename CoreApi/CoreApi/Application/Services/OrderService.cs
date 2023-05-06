using CoreApi.Infrastructure.Persistence;
using Domain.Orders;

namespace CoreApi.Application.Services;

public class OrderService : IOrderService
{

    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateOrder(Order order)
    {
        return await _unitOfWork.CreateOrder(order);
    }

    public async Task<Order> DeleteOrder(Guid id)
    {
        return await _unitOfWork.DeleteOrder(id);
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _unitOfWork.GetAllOrders();
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        return await _unitOfWork.GetOrderById(id);
    }

    public async Task<Order> UpdateOrder(Order updatedOrder)
    {
        return await _unitOfWork.UpdateOrder(updatedOrder);
    }
}
