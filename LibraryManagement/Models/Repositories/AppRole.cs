using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models.Repositories
{
    public class AppRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
