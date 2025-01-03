using CourseInside.Models;
using CourseInside.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var result = await _service.GetAllPaymentsAsync();
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _service.GetPaymentByIdAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            var result = await _service.AddPaymentAsync(payment);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment payment)
        {
            var result = await _service.UpdatePaymentAsync(payment);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePaymentAsync(id);
            if (!result.success)
            {
                return NotFound(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }
    }
}
