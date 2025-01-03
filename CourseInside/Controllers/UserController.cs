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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserModelDTO model)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            // Eğer admin değil ve kendi Id'sine de eşit değilse, güncelleme izni yok
            if (!isAdmin && currentUserId != id)
            {
                return Forbid(); // veya return Unauthorized("You cannot update another user.");
            }

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _userService.UpdateUserAsync(id, model);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");

            // Eğer admin değil ve kendi Id'sine de eşit değilse, silme izni yok
            if (!isAdmin && currentUserId != id)
            {
                return Forbid(); // veya return Unauthorized("You cannot delete another user.");
            }

            var result = await _userService.DeleteUserAsync(id);
            if (!result.success)
            {
                return BadRequest(new { Error = result.message });
            }

            return Ok(new { Message = result.message });
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
