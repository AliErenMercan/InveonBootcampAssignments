using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public class RolePermissionViewModel
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<PermissionCheckbox> Permissions { get; set; } = new List<PermissionCheckbox>();
    }

    public class PermissionCheckbox
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public bool IsAssigned { get; set; }
    }
}