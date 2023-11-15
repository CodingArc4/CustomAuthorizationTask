using Microsoft.AspNetCore.Identity;

namespace CustomAuthorizationTask.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
