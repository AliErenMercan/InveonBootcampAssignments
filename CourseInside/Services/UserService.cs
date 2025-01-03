using CourseInside.Controllers;
using CourseInside.Models;
using CourseInside.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CourseInside.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, ITokenHelper tokenHelper, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _logger = logger;
        }

        public async Task<ServiceResult> RegisterUserAsync(RegisterModel model)
        {
            if (!IsValidEmail(model.Email))
            {
                return ServiceResult.Failure("Invalid email format");
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Role = model.Role
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("User registration failed: {Errors}", string.Join(", ", result.Errors));
                return ServiceResult.Failure("Registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return ServiceResult.Success(user.Id);
        }

        public async Task<ServiceResult<TokenResult>> AuthenticateUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                _logger.LogWarning("Authentication failed for email: {Email}", model.Email);
                return ServiceResult<TokenResult>.Failure("Invalid credentials");
            }

            var token = _tokenHelper.GenerateToken(user.Id, user.Role);
            return ServiceResult<TokenResult>.Success(new TokenResult { Token = token, Role = user.Role });
        }

        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning("Password reset failed, user not found: {Email}", model.Email);
                return ServiceResult.Failure("User not found");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Password reset failed: {Errors}", string.Join(", ", result.Errors));
                return ServiceResult.Failure("Password reset failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<UserProfile>> GetUserProfileAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User profile access failed, user ID not found in claims");
                return ServiceResult<UserProfile>.Failure("User not authenticated");
            }

            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                _logger.LogWarning("User profile access failed, user not found: {UserId}", userId);
                return ServiceResult<UserProfile>.Failure("User not found");
            }

            var profile = new UserProfile
            {
                Name = appUser.Name!,
                Email = appUser.Email!,
                Role = appUser.Role!
            };

            return ServiceResult<UserProfile>.Success(profile);
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            return emailRegex.IsMatch(email);
        }
    }

    public class TokenResult
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
