using Microsoft.AspNetCore.Identity;

namespace Vietast.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
