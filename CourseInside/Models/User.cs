using Microsoft.AspNetCore.Identity;

namespace CourseInside.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}