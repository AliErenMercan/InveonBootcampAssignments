using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagement.Attributes
{
    public class PermissionAuthorizeAttribute(string requiredPermission) : Attribute, IActionFilter
    {
        private readonly string _requiredPermission = requiredPermission;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Dependency Injection ile UserManager ve PermissionRepository alınıyor
            var userManager = (UserManager<AppUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<AppUser>));
            var permissionRepository = (IPermissionRepository)context.HttpContext.RequestServices.GetService(typeof(IPermissionRepository));

            if (userManager == null || permissionRepository == null)
            {
                context.Result = new StatusCodeResult(500); // Internal Server Error
                return;
            }

            // Mevcut kullanıcıyı al
            var user = userManager.GetUserAsync(context.HttpContext.User).Result;
            if (user == null)
            {
                context.Result = new ForbidResult(); // Giriş yapılmamış
                return;
            }

            // Kullanıcının rollerine bağlı izinleri kontrol et
            var userPermissions = permissionRepository.GetPermissionsByUserIdAsync(user.Id).Result;

            if (!userPermissions.Contains(_requiredPermission))
            {
                context.Result = new ForbidResult(); // Gerekli izin yok
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Bu kısım boş bırakılabilir, işlem sonrası bir şey yapmayacağız
        }
    }
}
