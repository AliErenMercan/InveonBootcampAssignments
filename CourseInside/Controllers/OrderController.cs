using CourseInside.Models;
using CourseInside.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _service.GetAllOrdersAsync();
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _service.GetOrderByIdAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var result = await _service.AddOrderAsync(order);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var result = await _service.UpdateOrderAsync(order);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _service.DeleteOrderAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }
    }
}
