using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Repositories
{
    public class PermissionRepositoryWithSqlServer(AppDbContext context) : IPermissionRepository
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

    }

}
