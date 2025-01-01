using CourseInside.Models;
using CourseInside.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseInside.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Role = model.Role
            };

            await _userService.RegisterAsync(user, model.Password);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.LoginAsync(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Token oluştur
            var token = JwtTokenHelper.GenerateToken(user.Id, user.Role, _configuration);
            return Ok(new { Token = token });
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
}