using CourseInside.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _cartService.GetCartAsync(userId);
            if (!result.success)
                return BadRequest(new { Error = result.message });

            return Ok(result.data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _cartService.AddToCartAsync(userId, dto.CourseId, dto.Quantity);
            if (!result.success)
                return BadRequest(new { Error = result.message });

            return Ok(result.data);
        }

        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var result = await _cartService.RemoveFromCartAsync(cartItemId);
            if (!result.success)
                return BadRequest(new { Error = result.message });

            return Ok(new { Message = result.message });
        }
    }

    public class AddToCartDTO
    {
        public int CourseId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
