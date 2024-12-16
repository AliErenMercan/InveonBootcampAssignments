namespace LibraryManagement.Models.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task UpdateRolePermissionsAsync(Guid roleId, RolePermissionViewModel model);
        Task<IEnumerable<string>> GetPermissionsByUserIdAsync(Guid userId);
    }
}
