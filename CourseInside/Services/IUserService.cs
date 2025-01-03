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
        Task<ServiceResult<UserProfile>> GetUserProfileAsync(ClaimsPrincipal user);
    }
}
