using Microsoft.AspNetCore.Identity;

namespace TMDTFINAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
