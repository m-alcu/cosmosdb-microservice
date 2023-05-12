using CoreApi.Application.Services;
using Domain.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;

        private readonly IOrderService _orderService;

        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync(Order order)
        {
            return await _orderService.CreateOrder(order);
        }

        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            return await _orderService.UpdateOrder(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetByIdAsync(Guid id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order is null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var order = await _orderService.DeleteOrder(id);

            if (order is null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}