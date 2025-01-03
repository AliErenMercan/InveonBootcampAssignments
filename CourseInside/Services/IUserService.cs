using CourseInside.Controllers;
using CourseInside.Models;
using System.Security.Claims;

namespace CourseInside.Services
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterUserAsync(RegisterModel model);
        Task<ServiceResult<TokenResult>> AuthenticateUserAsync(LoginModel model);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordModel model);
        Task<ServiceResult<UserProfileDTO>> GetUserProfileAsync(ClaimsPrincipal user);

        Task<ServiceResult> UpdateUserAsync(string userId, UpdateUserModelDTO model);
        Task<ServiceResult> DeleteUserAsync(string userId);
    }

    public class UpdateUserModelDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }

    public class UserProfileDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public List<PurchasedCourseDTO> PurchasedCourses { get; set; } = new List<PurchasedCourseDTO>();
    }

    public class PurchasedCourseDTO
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

    public class TokenResult
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
