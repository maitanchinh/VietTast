using Vietast.Services.AuthAPI.Models.DTO;

namespace Vietast.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDTO> Register(RegistrationRequestDTO request);
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<bool> AssignRole(string email, string roleName);
    }
}
