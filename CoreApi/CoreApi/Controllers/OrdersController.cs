using CoreApi.Model;
using Cosmos.ModelBuilding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task PostAsync(Order order)
        {
            using (var context = new OrderContext())
            {
                await context.Database.EnsureCreatedAsync();

                context.Add(
                    new Order
                    {
                        Id = order.Id,
                        ShippingAddress = order.ShippingAddress,
                        PartitionKey = order.PartitionKey
                    });

                await context.SaveChangesAsync();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            using (var context = new OrderContext())
            {
                await context.Database.EnsureCreatedAsync();

                var orders = await context.Orders.ToListAsync();
                return Ok(orders);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetByIdAsync(int id)
        {
            using (var context = new OrderContext())
            {
                var order = await context.Orders.FindAsync(id, id.ToString());
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            using (var context = new OrderContext())
            {
                var order = await context.Orders.FindAsync(id, id.ToString());
                if (order == null)
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