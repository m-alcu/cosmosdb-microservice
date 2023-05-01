using CoreApi.Domain.Model;
using CoreApi.Infrastructure;
using CoreApi.Infrastructure.Database;

namespace CoreApi.Application.Services
{
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

        public void DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
