using LibraryManagement.Models.Repositories;

namespace LibraryManagement.Models
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }
        public AppRole Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
