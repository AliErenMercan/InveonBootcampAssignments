using CourseInside.Controllers;
using CourseInside.Models;
using CourseInside.Repositories;
using CourseInside.Utils;
using System.Text.RegularExpressions;

namespace CourseInside.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ITokenHelper tokenHelper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _logger = logger;
        }

        public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
        {
            if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _logger.LogWarning("Invalid email format {email}", dto.Email);
                return ServiceResult.Fail("E-posta formatı hatalı!");
            }
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Email already taken {email}", dto.Email);
                return ServiceResult.Fail("bu e-posta başka bir kullanıcı tarafından kullanılmış.");
            }
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = SecretHasher.Hash(dto.Password),
                Role = "User"
            };
            await _userRepository.AddAsync(user);
            _logger.LogInformation("User registered id={id}, email={email}", user.Id, user.Email);
            return ServiceResult.Ok("Kayıt başarıyla tamamlandı.");
        }

        public async Task<ServiceResult<TokenResult>> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found for login {email}", dto.Email);
                return ServiceResult<TokenResult>.Fail("Böyle bir kullanıcı bulunamadı!");
            }
            bool isPasswordValid = SecretHasher.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Invalid password for user {email}", dto.Email);
                return ServiceResult<TokenResult>.Fail("Geçersiz Şifre");
            }
            var token = _tokenHelper.GenerateToken(user.Id, user.Role);
            _logger.LogInformation("User login successful {id}", user.Id);
            var tokenResult = new TokenResult { Token = token, Role = user.Role };
            return ServiceResult<TokenResult>.Ok(tokenResult);
        }

        public async Task<ServiceResult<UserProfileDto>> GetUserProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User not found for profile {id}", userId);
                return ServiceResult<UserProfileDto>.Fail("Böyle bir kullanıcı bulunamadı!");
            }
            var purchasedList = user.Orders.Select(o => new PurchasedCourseDto
            {
                OrderId = o.Id,
                CourseTitle = o.Course.Title,
                PurchaseDate = o.OrderDate
            }).ToList();
            var profileDto = new UserProfileDto
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                PurchasedCourses = purchasedList
            };
            _logger.LogInformation("Profile retrieved for user {id}", user.Id);
            return ServiceResult<UserProfileDto>.Ok(profileDto);
        }

        public async Task<ServiceResult> UpdateUserAsync(UpdateUserDto dto, string currentUserId, bool isAdmin)
        {
            var user = await _userRepository.GetByIdAsync(currentUserId);
            if (user == null)
            {
                _logger.LogWarning("User not found during update {id}", currentUserId);
                return ServiceResult.Fail("Böyle bir kullanıcı bulunamadı!");
            }
            if (!string.IsNullOrEmpty(dto.Email))
            {
                if (!Regex.IsMatch(dto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    _logger.LogWarning("Invalid email format update {email}", dto.Email);
                    return ServiceResult.Fail("E-posta formatı hatalı!");
                }
                var existEmail = await _userRepository.GetByEmailAsync(dto.Email);
                if (existEmail != null && existEmail.Id != user.Id)
                {
                    _logger.LogWarning("Email already taken while updating {email}", dto.Email);
                    return ServiceResult.Fail("Bu e-posta başka bir kullanıcı tarafından kullanılmış.");
                }
                user.Email = dto.Email;
            }
            if (!string.IsNullOrEmpty(dto.Name))
            {
                user.Name = dto.Name;
            }
            if (!string.IsNullOrEmpty(dto.OldPassword) && !string.IsNullOrEmpty(dto.NewPassword))
            {
                bool oldValid = SecretHasher.Verify(dto.OldPassword, user.PasswordHash);
                if (!oldValid)
                {
                    _logger.LogWarning("Invalid old password for user {id}", user.Id);
                    return ServiceResult.Fail("Eski Parola Hatalı!");
                }
                user.PasswordHash = SecretHasher.Hash(dto.NewPassword);
            }
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("User updated {id}", user.Id);
            return ServiceResult.Ok("Kullanıcı Başarıyla Güncellendi");
        }

        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("User not found reset password {email}", dto.Email);
                return ServiceResult.Fail("Böyle bir kullanıcı bulunamadı!");
            }
            user.PasswordHash = SecretHasher.Hash(dto.NewPassword);
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("Password reset for user {id}", user.Id);
            return ServiceResult.Ok("Şifre Başarıyla Değiştirildi.");
        }
    }
}
