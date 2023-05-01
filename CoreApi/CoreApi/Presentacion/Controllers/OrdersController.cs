using CoreApi.Domain.Model;
using CoreApi.Infrastructure;
using CoreApi.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.External.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;

        private readonly AppSettingsService _appSettingsService;

        public OrdersController(ILogger<OrdersController> logger, AppSettingsService appSettingsService)
        {
            _logger = logger;
            _appSettingsService = appSettingsService;
        }

        [HttpPost]
        public async Task PostAsync(Order order)
        {
            using (var context = new OrderContext(_appSettingsService))
            {
                await context.Database.EnsureCreatedAsync();

                context.Add(
                    new Order(Guid.NewGuid(), order.TrackingNumber, order.ShippingAddress));

                await context.SaveChangesAsync();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            using (var context = new OrderContext(_appSettingsService))
            {
                await context.Database.EnsureCreatedAsync();

                var orders = await context.Orders.ToListAsync();
                return Ok(orders);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetByIdAsync(Guid id)
        {
            using (var context = new OrderContext(_appSettingsService))
            {
                var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
                if (order is null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            using (var context = new OrderContext(_appSettingsService))
            {
                var order = await context.Orders.FindAsync(id, id.ToString());
                if (order is null)
                {
                    return NotFound();
                }

                context.Orders.Remove(order);
                await context.SaveChangesAsync();

                return NoContent();
            }
        }

    }
}