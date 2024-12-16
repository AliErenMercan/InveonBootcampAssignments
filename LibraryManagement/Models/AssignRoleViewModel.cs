using LibraryManagement.Models.Repositories;

namespace LibraryManagement.Models
{
    public class AssignRoleViewModel
    {
        public string UserName { get; set; } = default!;
        public Guid UserId { get; set; }
        public List<AppRole> AvailableRoles { get; set; } = new List<AppRole>();
        public string? SelectedRole { get; set; }
        public bool UserFound { get; set; } = false;
        public bool SearchPerformed { get; set; } = false;
    }
}
