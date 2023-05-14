using CoreApi.Application.Caching;
using CoreApi.Infrastructure.Persistence;
using Domain.Entities.Orders;

namespace CoreApi.Application.Services;

public class OrderService : IOrderService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public OrderService(IUnitOfWork unitOfWork, ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    public async Task<Guid> CreateOrder(Order order)
    {
        return await _unitOfWork.CreateOrder(order);
    }

    public async Task<Order> DeleteOrder(Guid id)
    {
        return await _unitOfWork.DeleteOrder(id);
    }

    public async Task<IEnumerable<Order>> GetAllOrders(CancellationToken cancellationToken) =>

        //IEnumerable<Order>? orders = await _cacheService.GetAsync<IEnumerable<Order>>("orders", cancellationToken);

        //if (orders is not null)
        //{

        //    return orders;
        //}

        //orders = await _unitOfWork.GetAllOrders(cancellationToken);

        //await _cacheService.SetAsync("orders", orders, cancellationToken);

        //return orders;

        await _cacheService.GetAsync(
            "orders",
            async () =>
            {
                IEnumerable<Order>? orders = await _unitOfWork.GetAllOrders(cancellationToken);
                return orders;
            },
            cancellationToken);

    public async Task<Order> GetOrderById(Guid id)
    {
        return await _unitOfWork.GetOrderById(id);
    }

    public async Task<Order> UpdateOrder(Order updatedOrder)
    {
        return await _unitOfWork.UpdateOrder(updatedOrder);
    }
}
