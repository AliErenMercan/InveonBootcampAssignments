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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Choice()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var permissions = await permissionRepository.GetAllAsync();

            if (permissions == null)
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
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolePermissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await permissionRepository.CreateRoleWithPermissionsAsync(model);

            TempData["Message"] = "Role created successfully with permissions.";
            return RedirectToAction("ListRoles");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Role/DeleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole([FromRoute]Guid roleId)
        {
            await permissionRepository.DeleteRoleWithPermissionsAsync(roleId);
            TempData["Message"] = "Role deleted successfully.";
            return RedirectToAction("ListRoles");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Role/UpdateRole/{roleId}")]
        public async Task<IActionResult> UpdateRole([FromRoute] Guid roleId)
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
                    IsAssigned = role.RolePermissions.Any(rp => rp.PermissionId == p.Id)
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

            await permissionRepository.UpdateRolePermissionsAsync(role.Id, model);

            TempData["Message"] = "Role updated successfully.";
            return RedirectToAction("ListRoles");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignRole()
        {
            var roles = await roleManager.Roles.ToListAsync();
            var model = new AssignRoleViewModel
            {
                AvailableRoles = roles
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError("", "Username is required.");
                return View(model);
            }

            try
            {
                var result = await permissionRepository.AssignOrUpdateUserRoleAsync(model.UserName, model.SelectedRole);

                if (result)
                {
                    TempData["Message"] = "User role updated successfully.";
                    return RedirectToAction("AssignRole");
                }

                ModelState.AddModelError("", "Failed to update user role.");
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var roles = await roleManager.Roles.ToListAsync();
            model.AvailableRoles = roles;
            return View(model);
        }


    }
}
