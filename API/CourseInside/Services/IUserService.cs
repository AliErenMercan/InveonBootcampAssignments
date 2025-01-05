using CourseInside.Controllers;
using CourseInside.Models;

namespace CourseInside.Services
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterAsync(RegisterDto dto);
        Task<ServiceResult<TokenResult>> LoginAsync(LoginDto dto);
        Task<ServiceResult<UserProfileDto>> GetUserProfileAsync(string userId);
        Task<ServiceResult> UpdateUserAsync(UpdateUserDto dto, string currentUserId, bool isAdmin);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto);
    }

    public class TokenResult
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UserProfileDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<PurchasedCourseDto> PurchasedCourses { get; set; } = new();
    }

    public class PurchasedCourseDto
    {
        public int OrderId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
    }

    public class UpdateUserDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
