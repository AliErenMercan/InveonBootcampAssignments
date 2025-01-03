using CourseInside.Controllers;
using CourseInside.Models;
using CourseInside.Utils;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using CourseInside.Repositories;

namespace CourseInside.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserRepository _userRepository;

        public UserService( UserManager<User> userManager, ITokenHelper tokenHelper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
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
                return ServiceResult.Failure(
                    "Registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            return ServiceResult.Success(user.Id);
        }

        public async Task<ServiceResult<TokenResult>> AuthenticateUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
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
                return ServiceResult.Failure("User not found");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                return ServiceResult.Failure(
                    "Password reset failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<UserProfileDTO>> GetUserProfileAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return ServiceResult<UserProfileDTO>.Failure("User not authenticated");
            }

            var appUser = await _userRepository.GetUserWithOrdersAsync(userId);
            if (appUser == null)
            {
                return ServiceResult<UserProfileDTO>.Failure("User not found");
            }

            // Değişiklik: Artık "OrderItems" üzerinden kurs bilgisini alıyoruz
            var purchasedCourses = appUser.Orders
                .SelectMany(order => order.OrderItems, (order, orderItem) => new PurchasedCourseDTO
                {
                    CourseId = orderItem.CourseId,
                    Title = orderItem.Course.Title,
                    Price = orderItem.UnitPrice,
                    PurchaseDate = order.OrderDate
                })
                .ToList();

            var profile = new UserProfileDTO
            {
                Name = appUser.Name!,
                Email = appUser.Email!,
                Role = appUser.Role!,
                PurchasedCourses = purchasedCourses
            };

            return ServiceResult<UserProfileDTO>.Success(profile);
        }

        public async Task<ServiceResult> UpdateUserAsync(string userId, UpdateUserModelDTO model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Failure("User not found");
            }

            // Sadece ismini ve rolünü güncelliyoruz (ihtiyaca göre genişletilebilir)
            user.Name = model.Name;
            user.Role = model.Role;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return ServiceResult.Failure(
                    "User update failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            return ServiceResult.Success("User updated successfully");
        }

        public async Task<ServiceResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult.Failure("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return ServiceResult.Failure(
                    "User deletion failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            return ServiceResult.Success("User deleted successfully");
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            return emailRegex.IsMatch(email);
        }

    }
}
