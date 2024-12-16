using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Repositories
{
    public class PermissionRepositoryWithSqlServer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IPermissionRepository
    {
        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await context.Permissions.ToListAsync();
        }

        public async Task UpdateRolePermissionsAsync(Guid roleId, RolePermissionViewModel model)
        {
            var role = await context.Roles
                                    .Include(r => r.RolePermissions)
                                    .ThenInclude(rp => rp.Permission)
                                    .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            foreach (var permission in model.Permissions.Where(p => p.IsAssigned))
            {
                if (!role.RolePermissions.Any(rp => rp.PermissionId == permission.PermissionId))
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permission.PermissionId
                    };

                    context.RolePermissions.Add(rolePermission);
                }
            }

            foreach (var rolePermission in role.RolePermissions.ToList())
            {
                if (!model.Permissions.Any(p => p.PermissionId == rolePermission.PermissionId && p.IsAssigned))
                {
                    context.RolePermissions.Remove(rolePermission);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetPermissionsByUserIdAsync(Guid userId)
        {
            return await context.RolePermissions
                .Where(rp => context.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == rp.RoleId))
                .Select(rp => rp.Permission.PermissionName)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> AssignOrUpdateUserRoleAsync(string userName, string? selectedRole)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var userRoles = await userManager.GetRolesAsync(user);

            if (string.IsNullOrEmpty(selectedRole))
            {
                if (userRoles.Any())
                {
                    var removeResult = await userManager.RemoveFromRolesAsync(user, userRoles);
                    return removeResult.Succeeded;
                }
                return true;
            }

            if (userRoles.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return false;
            }

            var addResult = await userManager.AddToRoleAsync(user, selectedRole);
            return addResult.Succeeded;

        }

        public async Task CreateRoleWithPermissionsAsync(RolePermissionViewModel model)
        {
            var newRole = new AppRole
            {
                Name = model.RoleName,
                Description = model.Description
            };

            await roleManager.CreateAsync(newRole);

            foreach (var permission in model.Permissions.Where(p => p.IsAssigned))
            {
                var rolePermission = new RolePermission
                {
                    RoleId = newRole.Id,
                    PermissionId = permission.PermissionId
                };

                context.RolePermissions.Add(rolePermission);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteRoleWithPermissionsAsync(Guid roleId)
        {
            var role = await context.Roles
                                    .Include(r => r.RolePermissions)
                                    .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            var rolePermissions = role.RolePermissions.ToList();
            context.RolePermissions.RemoveRange(rolePermissions);

            context.Roles.Remove(role);

            await context.SaveChangesAsync();
        }

    }

}
