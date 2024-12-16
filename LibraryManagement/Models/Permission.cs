using LibraryManagement.Models.Repositories;

namespace LibraryManagement.Models
{
    public class Permission
    {
        public int Id { get; set; } // Primary Key
        public string PermissionName { get; set; } // Örneğin: "Viewer"
        public string Description { get; set; } // Örneğin: "Allows access to list books"

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
