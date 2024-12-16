using LibraryManagement.Models;
using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    public class RoleController(RoleManager<AppRole> roleManager, IPermissionRepository permissionRepository) : Controller
    {
        [HttpGet]
        public IActionResult Choice()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            // Veritabanından izinleri alıyoruz
            var permissions = await permissionRepository.GetAllAsync();

            if(permissions == null)
            {
                return View();
            }

            var model = new RolePermissionViewModel
            {
                Permissions = permissions.Select(p => new PermissionCheckbox
                {
                    PermissionId = p.Id,
                    PermissionName = p.PermissionName,
                    Description = p.Description,
                    IsAssigned = false
                }).ToList()
            };

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListRoles()
        {
            // Veritabanındaki tüm roller
            var roles = await roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Role/UpdateRole/{roleId}")]
        public async Task<IActionResult> UpdateRole([FromRoute]Guid roleId)
        {
            var role = await roleManager.Roles
                                .Include(r => r.RolePermissions)
                                .ThenInclude(rp => rp.Permission)
                                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return NotFound();
            }

            var permissions = await permissionRepository.GetAllAsync();
            var model = new RolePermissionViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Description = role.Description,
                Permissions = permissions.Select(p => new PermissionCheckbox
                {
                    PermissionId = p.Id,
                    PermissionName = p.PermissionName,
                    Description = p.Description,
                    IsAssigned = role.RolePermissions.Any(rp => rp.PermissionId == p.Id) // İzin var mı kontrolü
                }).ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RolePermissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await roleManager.FindByIdAsync(model.Id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.RoleName;
            role.Description = model.Description;

            // Permission güncellemelerini gerçekleştir
            await permissionRepository.UpdateRolePermissionsAsync(role.Id, model);

            TempData["Message"] = "Role updated successfully.";
            return RedirectToAction("ListRoles");
        }
    }
}
