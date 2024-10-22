using Vietast.Web.Models;

namespace Vietast.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<ResponseDTO?> AssignRoleAsync(RegistrationRequestDTO registrationRequestDTO);
    }
}
