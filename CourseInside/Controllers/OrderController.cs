using CourseInside.Models;
using CourseInside.Repositories;
using CourseInside.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var result = await _orderService.AddOrderAsync(order);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var result = await _orderService.UpdateOrderAsync(order);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [Authorize]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckoutCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Error = "User not found" });

            var result = await _orderService.CheckoutCartAsync(userId);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            // result.data -> totalAmount
            return Ok(new {Message = result.message, TotalAmount = result.data});
        }
    }
}
