using CourseInside.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userService.RegisterUserAsync(model);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = "User registered successfully", UserId = result.message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userService.AuthenticateUserAsync(model);
            if (!result.success)
            {
                return Unauthorized(new { Error = result.message });
            }

            return Ok(result.data);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userService.ResetPasswordAsync(model);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = "Password reset successfully" });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var result = await _userService.GetUserProfileAsync(User);
            if (!result.success)
            {
                return Unauthorized(new { Error = result.message });
            }

            return Ok(result.data);
        }
    }


    public class RegisterModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }

    public class LoginModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
