using Vietast.Services.AuthAPI.Models;

namespace Vietast.Services.AuthAPI.Services.IServices
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
